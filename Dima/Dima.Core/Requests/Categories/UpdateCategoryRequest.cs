using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Informe a descrição do novo título")]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}