using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Api.Handlers;

public class TransactionHandler : ITransactionHandler
{
    private readonly AppDbContext _context;

    public TransactionHandler(AppDbContext context)
        => _context = context;

    public async Task<Response<TransactionResponse>> CreateAsync(CreateTransactionRequest request)
    {
        var transaction = request.ToModel();

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return Response<TransactionResponse>.Success(transaction.ToResponse(), "Transação criada com sucesso!");
    }

    public Task<Response<TransactionResponse?>> DeleteAsync(DeleteTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<PagedResult<TransactionResponse>>> GetAllAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}