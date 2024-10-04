using Dima.Web.Security;

namespace Dima.Web.Pages.Identity;

public partial class RegisterComponentBase : ComponentBase
{
    [Inject] CookieAuthenticationStateProvider AuthState { get; init; } = null!;

    #region Refs
    protected MudForm? MudFormRef { get; set; }
    #endregion

    #region Methods
    protected static EmailAddressAttribute GetEmailValidator() => new() { ErrorMessage = "E-mail Inválido" };
    #endregion
}