using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class RegisterRequest
{
    [Required(ErrorMessage = "Informe o E-mail")]
    [EmailAddress(ErrorMessage = "E-mail Inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Informe a Senha")]
    public string Password { get; set; } = string.Empty;
}