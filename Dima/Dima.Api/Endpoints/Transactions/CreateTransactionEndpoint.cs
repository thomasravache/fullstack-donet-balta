using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma transaction")
            .WithDescription("Cria uma transaction")
            .WithOrder(1)
            .Produces<Response<TransactionResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        CreateTransactionRequest request
    )
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var response = await handler.CreateAsync(request);

        return response.IsSuccess
            ? Results.Created($"/{response.Data?.Id}", response)
            : Results.BadRequest(response);
    }
}