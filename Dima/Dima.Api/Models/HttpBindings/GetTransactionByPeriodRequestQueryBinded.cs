using System.Diagnostics.CodeAnalysis;
using System.Web;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Models.HttpBindings;
public class GetTransactionByPeriodRequestQueryBinded : GetTransactionByPeriodRequest, IParsable<GetTransactionByPeriodRequestQueryBinded>
{
    public static GetTransactionByPeriodRequestQueryBinded Parse(string s, IFormatProvider? provider)
    {
        var queryParams = HttpUtility.ParseQueryString(s);

        return new()
        {
            PageNumber = int.TryParse(queryParams["pageNumber"], out var pageNumber) ? pageNumber : default,
            PageSize = int.TryParse(queryParams["pageSize"], out var pageSize) ? pageSize : default,
            UserId = queryParams["userId"]?.ToString() ?? string.Empty,
            StartDate = DateTime.TryParse(queryParams["startDate"], out var startDate) ? startDate : default,
            EndDate = DateTime.TryParse(queryParams["endDate"], out var endDate) ? endDate : default,
        };
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GetTransactionByPeriodRequestQueryBinded result)
    {
        try
        {
            result = Parse(s ?? string.Empty, provider);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}