using Dima.Core.Requests;

namespace Dima.Api.Models.HttpBindings;

public class PagedRequestBinded : PagedRequest
{
    public static ValueTask<PagedRequestBinded?> BindAsync(HttpContext context)
    {
        return ValueTask.FromResult<PagedRequestBinded?>(new()
        {
            PageNumber = int.TryParse(context.Request.Query["pageNumber"], out var pageNumber) ? pageNumber : default,
            PageSize = int.TryParse(context.Request.Query["pageSize"], out var pageSize) ? pageSize : default,
            UserId = context.Request.Query["userId"].ToString() ?? string.Empty
        });
    }
}