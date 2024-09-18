using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.AddSerializationConfiguration();
builder.AddErrorHandlers();

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
