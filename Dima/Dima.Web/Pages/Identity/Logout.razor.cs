using Dima.Core.Handlers;
using Dima.Web.Security;

namespace Dima.Web.Pages.Identity;

public partial class LogoutComponentBase : ComponentBase
{
    #region Injections
    [Inject] private IAccountHandler AccountHandler { get; init; } = null!;
    [Inject] private ISnackbar Snackbar { get; init; } = null!;
    [Inject] private NavigationManager NavigationManager { get; init; } = null!;
    [Inject] private ICookieAuthenticationStateProvider AuthenticationStateProvider { get; init; } = null!;
    #endregion

    #region LifeCycle
    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await AccountHandler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        await base.OnInitializedAsync();
    }
    #endregion
}