using System.Text.Json.Serialization;
using Asp.Versioning;
using Dima.Api;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies(); // informar qual o tipo de autenticação (jwt, identity, etc)

builder.Services.AddAuthorization();

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
    ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => 
    {
        x.UseSqlServer(connectionString);
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


app.UseSwagger(); // informa que iremos utilizar o swagger
app.UseSwaggerUI(); // informa que iremos utilizar a UI do swagger

app.UseExceptionHandler();

app.MapGet("/", () => new { message = "OK" });
app.MapEndpoints();

app.Run();
