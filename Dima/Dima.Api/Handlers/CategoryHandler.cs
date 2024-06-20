using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;
using Microsoft.EntityFrameworkCore;

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

        return new Response<CategoryResponse>(
            category.ToResponse(),
            StatusCodes.Status201Created,
            "Categoria criada com sucesso!");
    }

    public async Task<Response<CategoryResponse?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        if (category is null)
            return new Response<CategoryResponse?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return new Response<CategoryResponse?>(category.ToResponse(), message: "Categoria removida com sucesso!");
    }

    public async Task<PagedResponse<List<CategoryResponse>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        var categories = await _context.Categories
            .Where(x => x.UserId == request.UserId)
            .Select(x => x.ToResponse())
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<List<CategoryResponse>>(categories);
    }

    public async Task<Response<CategoryResponse?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        if (category is null)
            return new Response<CategoryResponse?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada");
        
        return new Response<CategoryResponse?>(category.ToResponse());
    }

    public async Task<Response<CategoryResponse?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        if (category is null)
            return new Response<CategoryResponse?>(null, StatusCodes.Status404NotFound, "Categoria não encontrada");

        category.FillModel(request);

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return new Response<CategoryResponse?>(category.ToResponse(), message: "Categoria editada com sucesso");
    }
}