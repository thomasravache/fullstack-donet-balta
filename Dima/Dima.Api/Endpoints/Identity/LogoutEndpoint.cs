using Dima.Api.Common.Api;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", HandleAsync) 
            .WithName("Identity: Logout")
            .WithSummary("Realiza o logout do usuário")
            .WithDescription("Realiza o logout do usuário")
            .RequireAuthorization();
    }

    public static async Task<IResult> HandleAsync(
        SignInManager<User> signInManager,
        UserManager<User> _a,
        RoleManager<IdentityRole<long>> _b
    )
    {
        // limpar cookies de navegação e o signout será feito
        await signInManager.SignOutAsync();

        return Results.Ok();
    }
}