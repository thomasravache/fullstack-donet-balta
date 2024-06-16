var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
x.CustomSchemaIds(n => n.FullName); // Pega o nome das classes para documentação, evitando conflitos entre nomes iguais de classes
});

var app = builder.Build();

app.UseSwagger(); // informa que iremos utilizar o swagger
app.UseSwaggerUI(); // informa que iremos utilizar a UI do swagger

app.MapGet("/v1/categories", () => "Hello World!");
app.MapGet("/v2/categories", () => "Hello World!");
app.MapPost("/v1/categories", () => "Hello World!");
app.MapPut("/v1/categories", () => "Hello World!");
app.MapDelete("/v1/categories", () => "Hello World!");

app.Run();
