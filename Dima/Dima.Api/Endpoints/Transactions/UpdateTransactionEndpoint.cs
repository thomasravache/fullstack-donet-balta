using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Edita uma transaction")
            .WithDescription("Edita uma tranction")
            .WithOrder(4)
            .Produces<Response<TransactionResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        UpdateTransactionRequest request,
        long id
    )
    {
        request.Id = id;
        var response = await handler.UpdateAsync(request);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}
