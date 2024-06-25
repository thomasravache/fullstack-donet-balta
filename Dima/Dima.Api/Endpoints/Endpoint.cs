using Asp.Versioning;
using Asp.Versioning.Builder;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Filters;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var endpoints = app
            .MapGroup("api/")
            .WithApiVersionSet(apiVersionSet)
            .AddEndpointFilter<ValidateModelFilter>();

        endpoints.MapGroup("v{version:apiVersion}/categories")
            .WithTags("Categories")
            .MapToApiVersion(1)
            .MapEndpoint<CreateCategoryEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);

        return app;
    }
}