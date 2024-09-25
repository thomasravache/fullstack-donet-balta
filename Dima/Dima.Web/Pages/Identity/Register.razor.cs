namespace Dima.Web.Pages.Identity;

public class RegisterComponentBase : ComponentBase
{
    #region Refs
    protected MudForm? MudFormRef { get; set; }
    #endregion

    #region Methods
    protected static EmailAddressAttribute GetEmailValidator() => new() { ErrorMessage = "E-mail Inválido" };
    #endregion
}