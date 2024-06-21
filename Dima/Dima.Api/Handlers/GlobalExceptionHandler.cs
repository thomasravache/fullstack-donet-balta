using Dima.Core.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Dima.Api.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Exception {Message}",
            exception.Message
        );

        var response = new Response<object>(null, StatusCodes.Status500InternalServerError, "Erro interno do servidor");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}