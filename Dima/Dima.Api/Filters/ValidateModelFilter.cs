
using Dima.Core.Extensions;
using Dima.Core.Responses;

namespace Dima.Api.Filters;

public class ValidateModelFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        List<string> validationMessages = [];

        foreach (var argument in context.Arguments)
            validationMessages.AddRange(argument?.GetErrorsFromValidationContext() ?? []);

        if (validationMessages.Count == 0) return await next(context);

        var response = new Response<object>(null, StatusCodes.Status400BadRequest, validationMessages);

        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.HttpContext.Response.WriteAsJsonAsync(response);

        return await next(context);
    }
}