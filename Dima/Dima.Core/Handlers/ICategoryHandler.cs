using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<CategoryResponse>> CreateAsync(CreateCategoryRequest request);
    Task<Response<CategoryResponse>> DeleteAsync(DeleteCategoryRequest request);
    Task<PagedResponse<List<CategoryResponse>>> GetAllAsync(GetAllCategoriesRequest request);
    Task<Response<CategoryResponse>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<Response<CategoryResponse>> UpdateAsync(UpdateCategoryRequest request);
}