using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Pages.Identity;

public partial class RegisterComponentBase : ComponentBase
{
    #region Injections
    [Inject] private IAccountHandler AccountHandler { get; init; } = null!;
    [Inject] private ISnackbar Snackbar { get; init; } = null!;
    [Inject] private NavigationManager NavigationManager { get; init; } = null!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; init; } = null!;
    #endregion

    #region Refs
    protected MudForm? MudFormRef { get; set; }
    #endregion

    #region Methods
    protected static EmailAddressAttribute GetEmailValidator() => new() { ErrorMessage = "E-mail Inválido" };
    #endregion
}