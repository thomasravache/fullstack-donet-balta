using Asp.Versioning;
using Asp.Versioning.Builder;
using Azure;
using Dima.Api.Data;
using Dima.Api.Filters;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// todo: api versioning https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
// https://github.com/dotnet/aspnet-api-versioning/blob/3857a332057d970ad11bac0edfdbff8a559a215d/examples/AspNetCore/WebApi/MinimalOpenApiExample/Program.cs
// todo: grouproutes https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/route-handlers?view=aspnetcore-8.0#route-groups

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
    ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => 
    {
        x.UseSqlServer(connectionString);
    });

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName); // Pega o nome das classes para documentação, evitando conflitos entre nomes iguais de classes
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
// builder.Services.AddExc

var app = builder.Build();


app.UseSwagger(); // informa que iremos utilizar o swagger
app.UseSwaggerUI(); // informa que iremos utilizar a UI do swagger

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

app.UseExceptionHandler();

app.MapPost("api/v{v:apiVersion}/categories", async ([FromBody] CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreateAsync(request))
    .AddEndpointFilter<ValidateModelFilter>()
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1)
    .WithName("Categories: Create")
    .WithSummary("Cria uma categoria")
    .Produces<Response<CategoryResponse>>();


app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) => 
{
    request.Id = id;
    return await handler.UpdateAsync(request);
})
    .AddEndpointFilter<ValidateModelFilter>()
    .WithName("Categories: Update")
    .WithSummary("Edita uma categoria")
    .Produces<Response<CategoryResponse?>>();

app.MapGet("/v1/categories/{id}", async ([FromRoute] long id, [FromBody] GetCategoryByIdRequest request, ICategoryHandler handler) => 
{
    request.Id = id;
    return await handler.GetByIdAsync(request);
})
    .WithName("Categories: GetById")
    .WithSummary("Obtém uma categoria por Id")
    .Produces<Response<CategoryResponse?>>();

app.MapGet("/v1/categories", async (ICategoryHandler handler) => 
{
    GetAllCategoriesRequest request = new()
    {
        UserId = "thomao@gmail.com"
    };
    return await handler.GetAllAsync(request);
})
    .WithName("Categories: Get All")
    .WithSummary("Obtém todas as categorias de um usuário")
    .Produces<Response<CategoryResponse?>>();

app.MapDelete("/v1/categories/{id}", async ([FromRoute] long id, [FromBody] DeleteCategoryRequest request, ICategoryHandler handler) => 
{
    request.Id = id;
    return await handler.DeleteAsync(request);
})
    .WithName("Categories: Delete")
    .WithSummary("Deleta uma categoria")
    .Produces<Response<CategoryResponse?>>();

// app.MapGroup("/v1/categories

app.Run();
