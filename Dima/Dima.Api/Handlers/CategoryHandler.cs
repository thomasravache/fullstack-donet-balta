using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;

namespace Dima.Api.Handlers;

public class CategoryHandler : ICategoryHandler
{
    private readonly AppDbContext _context;

    public CategoryHandler(AppDbContext context)
        => _context = context;

    public async Task<Response<CategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var category = request.ToModel();

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return new Response<CategoryResponse>(category.ToResponse(), StatusCodes.Status200OK);
    }

    public Task<Response<CategoryResponse>> DeleteAsync(DeleteCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<List<CategoryResponse>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<CategoryResponse>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<CategoryResponse>> UpdateAsync(UpdateCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}