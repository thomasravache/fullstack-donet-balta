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

    public async Task<Response<TransactionResponse>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/v1/transactions", request);

        return await result.Content.ReadFromJsonAsync<Response<TransactionResponse>>()
            ?? Response<TransactionResponse>.Failure("Falha ao criar transação");
    }

    public async Task<Response<TransactionResponse?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _client.DeleteAsync($"api/v1/transactions/{request.Id}");

        return await result.Content.ReadFromJsonAsync<Response<TransactionResponse?>>()
            ?? Response<TransactionResponse?>.Failure("Falha ao excluir transação");
    }

    public Task<Response<PagedResult<TransactionResponse>>> GetAllAsync(GetAllTransactionsRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<TransactionResponse?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        return await _client.GetFromJsonAsync<Response<TransactionResponse?>>($"api/v1/transactions/{request.Id}")
            ?? Response<TransactionResponse?>.Failure("Falha ao obter transação por Id");
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

    public async Task<Response<TransactionResponse?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _client.PutAsJsonAsync($"api/v1/transactions/{request.Id}", request);

        return await result.Content.ReadFromJsonAsync<Response<TransactionResponse?>>()
            ?? Response<TransactionResponse?>.Failure("Falha ao atualizar transação");
    }
}