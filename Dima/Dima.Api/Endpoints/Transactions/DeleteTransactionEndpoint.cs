using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Deleta uma transaction")
            .WithDescription("Deleta uma transaction")
            .WithOrder(2)
            .Produces<Response<TransactionResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        [FromBody] DeleteTransactionRequest request,
        long id
    )
    {

        var response = await handler.DeleteAsync(new()
        {
            Id = id,
            UserId = request.UserId
        });

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}