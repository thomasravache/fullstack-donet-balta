using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Categories;

namespace Dima.Core.Extensions;

public static class CategoryExtensions
{
    public static CategoryResponse ToResponse(this Category entity)
        => new CategoryResponse
        {
            Description = entity.Description,
            Id = entity.Id,
            Title = entity.Title,
            UserId = entity.UserId
        };

    public static Category ToModel(this CreateCategoryRequest request)
        => new Category
        {
            Description = request.Description,
            Title = request.Title,
            UserId = request.UserId
        };
}