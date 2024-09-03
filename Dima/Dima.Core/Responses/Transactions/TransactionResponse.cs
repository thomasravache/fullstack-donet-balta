using Dima.Core.Enums;
using Dima.Core.Models;
using Dima.Core.Responses.Categories;

namespace Dima.Core.Responses.Transactions;

public class TransactionResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw; // Geralmente se tem mais despesas do que receitas
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public CategoryResponse? Category { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
}