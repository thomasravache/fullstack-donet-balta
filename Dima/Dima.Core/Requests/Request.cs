using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests;

public abstract class Request
{
    [Required(ErrorMessage = "O Id do usuário é obrigatório")]
    [EmailAddress(ErrorMessage = "O Id do usuário precisa ser um e-mail válido")]
    public required string UserId { get; set; } = string.Empty;
}