using System.Security.Claims;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName); // Pega o nome das classes para documentação, evitando conflitos entre nomes iguais de classes
});

// tem que ser nessa ordem o authentication e authorization
builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies(); // informar qual o tipo de autenticação (jwt, identity, etc)

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(x => 
    {
        x.UseSqlServer(Configuration.ConnectionString);
    });

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger(); // informa que iremos utilizar o swagger
app.UseSwaggerUI(); // informa que iremos utilizar a UI do swagger

app.UseExceptionHandler();

app.MapGet("/", () => new { message = "OK" });
app.MapEndpoints();

app.MapGroup("api/v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>(); // endpoints padrão do Identity

app.MapGroup("api/v1/identity")
    .WithTags("Identity")
    .MapPost("/logout", async (SignInManager<User> signInManager, UserManager<User> _a, RoleManager<IdentityRole<long>> _b) =>
    {
        // limpar cookies de navegação e o signout será feito
        await signInManager.SignOutAsync();

        return Results.Ok();
    })
    .RequireAuthorization();

app.MapGroup("api/v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", async (
        SignInManager<User> _,
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        ClaimsPrincipal signedUser
        ) =>
    {
        // limpar cookies de navegação e o signout será feito
        var user = await userManager.FindByEmailAsync(""); // buscar pelo banco o usuario pelo email
        var userRoles = user?.Roles; // roles do usuário pegos pelo banco de dados

        // ou usando o roleManager
        var roles = await roleManager.Roles.ToListAsync(); // todas as roles existentes

        // obter usuário logado com o ClaimsPrincipal
        
        if (signedUser.Identity is null || !signedUser.Identity.IsAuthenticated)
            return Results.Unauthorized();

        // aqui pega os roles pelos cookies sem precisar pegar no banco
            // vantagem: não precisa bater no banco
            // desvantagem: se o tempo de expiração do cookie for muito grande ele vai retornar que o usuário tenha uma permissão que já tenha sido retirada até o cookie expirar
        var identity = (ClaimsIdentity)signedUser.Identity;
        var signedUserRoles = identity.FindAll(identity.RoleClaimType)
            .Select(x => new
            {
                x.Issuer, // quem emitiu
                x.OriginalIssuer,
                x.Type, // tipo do claim
                x.Value,
                x.ValueType
            });

        return Results.Ok(new
        {
            roles,
            signedUser = new
            {
                roles = signedUserRoles
            }
        });
    })
    .RequireAuthorization();


app.Run();
