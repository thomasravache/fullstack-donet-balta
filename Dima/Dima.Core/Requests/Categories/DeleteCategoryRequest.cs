using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest : Request
{
    [Required(ErrorMessage = "O id da categoria é obrigatório")]
    public long Id { get; set; }
}