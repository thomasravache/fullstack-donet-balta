using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Obtém uma categoria pelo Id")
            .WithDescription("Obtém uma categoria pelo Id")
            .WithOrder(4)
            .Produces<Response<CategoryResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        long id
    )
    {
        var result = await handler.GetByIdAsync(new()
        {
            Id = id,
            UserId = "maneiro@gmail.com"
        });

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}