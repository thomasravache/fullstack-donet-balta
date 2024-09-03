using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Obtém todas categoria paginadas")
            .WithDescription("Obtém todas categoria paginadas")
            .WithOrder(5)
            .Produces<Response<PagedResult<CategoryResponse>>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [AsParameters] GetAllCategoriesRequest request
    )
    {
        var result = await handler.GetAllAsync(new()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            UserId = request.UserId
        });

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}