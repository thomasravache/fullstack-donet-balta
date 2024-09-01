using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses.Transactions;

namespace Dima.Core.Extensions;

public static class TransactionExtensions
{
    public static TransactionResponse ToResponse(this Transaction entity)
        => new()
        {
            Amount = entity.Amount,
            Category = entity.Category,
            CategoryId = entity.CategoryId,
            CreatedAt = entity.CreatedAt,
            Id = entity.Id,
            PaidOrReceivedAt = entity.PaidOrReceivedAt,
            Title = entity.Title,
            Type = entity.Type,
            UserId = entity.UserId
        };

    public static Transaction ToModel(this CreateTransactionRequest request)
        => new()
        {
            Amount = request.Amount,
            CategoryId = request.CategoryId,
            Title = request.Title,
            Type = request.Type,
            PaidOrReceivedAt = request.PaidOrReceivedAt,
            UserId = request.UserId
        };

    public static void FillModel(this Transaction model, UpdateTransactionRequest request)
    {
        model.Amount = request.Amount;
        model.Title = request.Title;
        model.Type = request.Type;
        model.CategoryId = request.CategoryId;
        model.PaidOrReceivedAt = request.PaidOrReceivedAt;
    }
}