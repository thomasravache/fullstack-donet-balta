using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;

namespace Dima.Web.Pages.Identity;

public partial class LoginComponentBase : ComponentBase
{
    #region Injections
    [Inject] private IAccountHandler AccountHandler { get; init; } = null!;
    [Inject] private ISnackbar Snackbar { get; init; } = null!;
    [Inject] private NavigationManager NavigationManager { get; init; } = null!;
    [Inject] private ICookieAuthenticationStateProvider AuthenticationStateProvider { get; init; } = null!;
    #endregion

    #region Refs
    protected MudForm? MudFormRef { get; set; }
    #endregion

    #region Props
    protected bool IsBusy { get; set; } = false;
    protected LoginRequest InputModel { get; set; } = new();
    #endregion

    #region LifeCycle
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user is { Identity.IsAuthenticated: true })
            NavigationManager.NavigateTo(PageRoutes.Comecar);
    }
    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await AccountHandler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo(PageRoutes.Home);
            }
            else
                Snackbar.Add(result.PrincipalMessage ?? "Erro ao realizar o login", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}