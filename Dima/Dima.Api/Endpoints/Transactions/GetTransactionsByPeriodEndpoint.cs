using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Models.HttpBindings;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/by-period", HandleAsync)
            .WithName("Transactions: GetByPeriod")
            .WithSummary("Obtém transações por período")
            .WithDescription("Obtém transações por período")
            .WithOrder(3)
            .Produces<Response<TransactionResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        [AsParameters] GetTransactionByPeriodRequest request
    )
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var response = await handler.GetByPeriodAsync(request);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.BadRequest(response);
    }
}
