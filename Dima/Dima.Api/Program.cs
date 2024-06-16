var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/v1/categories", () => "Hello World!");
app.MapGet("/v2/categories", () => "Hello World!");
app.MapPost("/v1/categories", () => "Hello World!");
app.MapPut("/v1/categories", () => "Hello World!");
app.MapDelete("/v1/categories", () => "Hello World!");

app.Run();
