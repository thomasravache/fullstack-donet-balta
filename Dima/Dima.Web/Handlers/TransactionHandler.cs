using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Web.Handlers;

public class TransactionHandler : ITransactionHandler
{
    public Task<Response<TransactionResponse>> CreateAsync(CreateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> DeleteAsync(DeleteTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<PagedResult<TransactionResponse>>> GetAllAsync(GetAllTransactionsRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<PagedResult<TransactionResponse>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}