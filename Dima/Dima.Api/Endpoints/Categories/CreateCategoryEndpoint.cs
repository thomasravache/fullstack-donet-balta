using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma categoria")
            .WithDescription("Cria uma categoria")
            .WithOrder(1)
            .Produces<Response<CategoryResponse>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        CreateCategoryRequest request
    )
    {
        var response = await handler.CreateAsync(request);

        return response.IsSuccess
         ? Results.Created($"/{response.Data?.Id}", response)
         : Results.BadRequest(response);
    }
}