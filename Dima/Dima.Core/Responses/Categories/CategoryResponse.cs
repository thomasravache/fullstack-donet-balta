using Dima.Core.Responses.Transactions;

namespace Dima.Core.Responses.Categories;

public class CategoryResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<TransactionResponse> Transactions { get; set; } = [];
}