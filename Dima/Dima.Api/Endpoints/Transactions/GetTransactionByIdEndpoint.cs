using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Obtém uma transação pelo Id")
            .WithDescription("Obtém uma transação pelo Id")
            .WithOrder(5)
            .Produces<Response<TransactionResponse?>>();
            
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        long id
    )
    {
        var result = await handler.GetByIdAsync(new()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        });

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}