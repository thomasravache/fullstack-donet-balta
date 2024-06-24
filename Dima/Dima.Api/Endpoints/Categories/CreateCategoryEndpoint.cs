using Dima.Api.Common.Api;
using Dima.Api.Filters;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .AddEndpointFilter<ValidateModelFilter>()
            .MapToApiVersion(1)
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