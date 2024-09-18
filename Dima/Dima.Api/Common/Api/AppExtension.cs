using System.Security.Claims;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger(); // informa que iremos utilizar o swagger
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("swagger/v1/swagger.json", "Dima API 1.0");
            // c.SwaggerEndpoint("swagger/v2/swagger.json", "Dima API 2.0");
            c.RoutePrefix = string.Empty;
        }); // informa que iremos utilizar a UI do swagger
        app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

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
    }

    public static void UseErrorHandlers(this WebApplication app)
    {
        app.UseExceptionHandler();
    }
}