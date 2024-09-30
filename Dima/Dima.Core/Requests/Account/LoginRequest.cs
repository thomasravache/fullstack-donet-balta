using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account;

public class LoginRequest
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail Inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha Inválida")]
    public string Password { get; set; } = string.Empty;
}