using System.Security.Claims;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger(); // informa que iremos utilizar o swagger
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("swagger/v1/swagger.json", "Dima API 1.0");
            // c.SwaggerEndpoint("swagger/v2/swagger.json", "Dima API 2.0");
            c.RoutePrefix = string.Empty;
        }); // informa que iremos utilizar a UI do swagger
        app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void UseErrorHandlers(this WebApplication app)
    {
        app.UseExceptionHandler();
    }
}