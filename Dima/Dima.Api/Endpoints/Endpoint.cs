using Asp.Versioning;
using Asp.Versioning.Builder;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Identity;
using Dima.Api.Endpoints.Transactions;
using Dima.Api.Filters;
using Dima.Api.Models;

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

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v{version:apiVersion}/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>()
            .MapIdentityApi<User>(); // endpoints padrão do Identity;

        // Quando usamos o MapIdentityApi quer dizer que estamos utilizando os endpoints padrão do identity
            // é possível fazer isso customizado e mapear da forma que você quiser

        endpoints.MapGroup("v{version:apiVersion}/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapToApiVersion(1)
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v{version:apiVersion}/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapToApiVersion(1)
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionsByPeriodEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);

        return app;
    }
}