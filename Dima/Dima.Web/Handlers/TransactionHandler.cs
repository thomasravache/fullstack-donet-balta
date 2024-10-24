using System.Net.Http.Json;
using Dima.Core.Common.Utils;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Dima.Core.Responses.Transactions;

namespace Dima.Web.Handlers;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

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

    public async Task<Response<PagedResult<TransactionResponse>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        const string format = "yyyy-MM-dd";

        var startDate = request.StartDate is not null
            ? request.StartDate.Value.ToString(format)
            : DateTime.Now.GetFirstDay().ToString(format);

        var endDate = request.EndDate is not null
            ? request.EndDate.Value.ToString(format)
            : DateTime.Now.GetLastDay().ToString(format);

        var query = new QueryStringBuilder()
            .AddQueryParameter("startDate", startDate)
            .AddQueryParameter("endDate", endDate)
            .BuildQuery();

        var url = "api/v1/transactions" + query;

        return await _client.GetFromJsonAsync<Response<PagedResult<TransactionResponse>>>(url)
            ?? Response<PagedResult<TransactionResponse>>.Failure("Falha ao obter transações por período");
    }

    public Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}