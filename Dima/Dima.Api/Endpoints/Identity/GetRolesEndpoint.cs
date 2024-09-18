using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles", HandleAsync)
        .WithName("Identity: Get Roles")
        .WithSummary("Obtém Roles do usuário logado")
        .WithDescription("Obtém Roles do usuário logado")
        .RequireAuthorization();
    }

    public static async Task<IResult> HandleAsync(
        SignInManager<User> _,
        UserManager<User> userManager,
        RoleManager<IdentityRole<long>> roleManager,
        ClaimsPrincipal signedUser
    )
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
    }
}
