namespace Dima.Core.Responses;

public class PagedResult<TData>
    where TData : class
{
    public required List<TData> Items { get; set; } = [];
    public required int TotalCount { get; set; }
    public required int CurrentPage { get; set; }
    public required int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public static PagedResult<TData> EmptyResult()
        => new()
        {
            CurrentPage = default,
            Items = [],
            PageSize = default,
            TotalCount = default
        };
}