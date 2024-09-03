using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Core.Handlers;

public interface ITransactionHandler
{
    Task<Response<TransactionResponse>> CreateAsync(CreateTransactionRequest request);
    Task<Response<TransactionResponse?>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<PagedResult<TransactionResponse>>> GetAllAsync(GetAllTransactionsRequest request);
    Task<Response<TransactionResponse?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<Response<PagedResult<TransactionResponse>>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
    Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request);
}