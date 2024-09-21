using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public class LoginComponentBase : ComponentBase
{
    #region Refs
    protected MudForm? MudFormRef { get; set; }
    #endregion

    #region Methods
    protected static EmailAddressAttribute GetEmailValidator() => new() { ErrorMessage = "E-mail Inválido" };
    #endregion
}