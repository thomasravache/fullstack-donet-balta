using Azure;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
    ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => 
    {
        x.UseSqlServer(connectionString);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName); // Pega o nome das classes para documentação, evitando conflitos entre nomes iguais de classes
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger(); // informa que iremos utilizar o swagger
app.UseSwaggerUI(); // informa que iremos utilizar a UI do swagger

app.MapPost("/v1/categories", ([FromBody] CreateCategoryRequest request, ICategoryHandler handler) => handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma categoria")
    .Produces<Response<CategoryResponse>>();
app.MapGet("/v2/categories", () => "Hello World!");
app.MapPut("/v1/categories", ([FromBody] UpdateCategoryRequest request, ICategoryHandler handler) => handler.UpdateAsync(request))
    .WithName("Categories: Update")
    .WithSummary("Edita uma categoria")
    .Produces<Response<CategoryResponse?>>();
app.MapDelete("/v1/categories", () => "Hello World!");

app.Run();
