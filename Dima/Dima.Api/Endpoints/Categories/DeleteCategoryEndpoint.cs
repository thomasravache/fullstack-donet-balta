using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<CategoryResponse?>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        long id
    )
    {
        var result = await handler.DeleteAsync(new()
        {
            Id = id,
            UserId = "maneiro@gmail.com"
        });

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}