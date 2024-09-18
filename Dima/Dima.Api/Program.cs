using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.AddSerializationConfiguration();
builder.AddErrorHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseSecurity();

app.UseErrorHandlers();

app.MapEndpoints();

app.Run();
