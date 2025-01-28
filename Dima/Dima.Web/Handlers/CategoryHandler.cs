using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Web.Handlers;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<CategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/v1/categories", request);

        return await result.Content.ReadFromJsonAsync<Response<CategoryResponse>>()
            ?? Response<CategoryResponse>.Failure("Falha ao criar categoria.");
    }

    public async Task<Response<CategoryResponse?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _client.DeleteAsync($"api/v1/categories/{request.Id}");

        return await result.Content.ReadFromJsonAsync<Response<CategoryResponse?>>()
            ?? Response<CategoryResponse?>.Failure("Falha ao excluir categoria.");
    }

    public async Task<Response<PagedResult<CategoryResponse>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        return await _client.GetFromJsonAsync<Response<PagedResult<CategoryResponse>>>("v1/categories")
            ?? Response<PagedResult<CategoryResponse>>.Failure("Falha ao obter categorias");   
    }

    public async Task<Response<CategoryResponse?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        return await _client.GetFromJsonAsync<Response<CategoryResponse?>>($"v1/categories/{request.Id}")
            ?? Response<CategoryResponse?>.Failure("Falha ao obter categoria por Id");
    }

    public async Task<Response<CategoryResponse?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var result = await _client.PutAsJsonAsync($"api/v1/categories/{request.Id}", request);

        return await result.Content.ReadFromJsonAsync<Response<CategoryResponse?>>()
            ?? Response<CategoryResponse?>.Failure("Falha ao atualizar categoria.");
    }
}
