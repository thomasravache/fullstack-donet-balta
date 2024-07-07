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

        return Response<CategoryResponse>.Success(category.ToResponse(), "Categoria criada com sucesso!");
    }

    public async Task<Response<CategoryResponse?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        if (category is null)
            return Response<CategoryResponse?>.Failure("Categoria não encontrada");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Response<CategoryResponse?>.Success(category.ToResponse(), message: "Categoria removida com sucesso!");
    }

    public async Task<Response<PagedResult<CategoryResponse>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        var query = _context.Categories
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId);

        var categories = await query
            .OrderBy(category => category.Title)
            .Select(category => category.ToResponse())
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var count = await query.CountAsync();

        var result = new PagedResult<CategoryResponse>()
        {
            CurrentPage = request.PageNumber,
            Items = categories,
            PageSize = request.PageSize,
            TotalCount = count
        };

        return Response<PagedResult<CategoryResponse>>.Success(result);
    }

    public async Task<Response<CategoryResponse?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        return category is null
            ? Response<CategoryResponse?>.Failure("Categoria não encontrada")
            : Response<CategoryResponse?>.Success(category.ToResponse());
    }

    public async Task<Response<CategoryResponse?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId);

        if (category is null)
            return Response<CategoryResponse?>.Failure("Categoria não encontrada");

        category.FillModel(request);

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Response<CategoryResponse?>.Success(category.ToResponse(), message: "Categoria editada com sucesso");
    }
}