using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedResult<TData>
    where TData : class
{
    public required List<TData> Items { get; set; } = [];
    public required int TotalCount { get; set; }
    public required int CurrentPage { get; set; }
    public required int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}