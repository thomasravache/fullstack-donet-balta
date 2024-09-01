using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Response<TransactionResponse?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var transaction = await _context.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(transaction =>
                transaction.Id == request.Id &&
                transaction.UserId == request.UserId
            );

        if (transaction is null)
            return Response<TransactionResponse?>.Failure("Transação não encontrada");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return Response<TransactionResponse?>.Success(transaction.ToResponse(), message: "Transação removida com sucesso!");
    }

    public Task<Response<PagedResult<TransactionResponse>>> GetAllAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var transaction = await _context.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(transaction =>
                transaction.Id == request.Id &&
                transaction.UserId == request.UserId
            );

        if (transaction is null)
            return Response<TransactionResponse?>.Failure("Categoria não encontrada");

        transaction.FillModel(request);

        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();

        return Response<TransactionResponse?>.Success(transaction.ToResponse(), message: "Transação editada com sucesso!");
    }
}