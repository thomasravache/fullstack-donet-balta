using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
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

    #region Props
    protected bool IsBusy { get; set; } = false;
    protected RegisterRequest InputModel { get; set; } = new();
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
    protected static EmailAddressAttribute GetEmailValidator() => new() { ErrorMessage = "E-mail Inválido" };

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await AccountHandler.RegisterAsync(InputModel);

            if (result.IsSuccess)
            {
                Snackbar.Add("Registro efetuado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo(PageRoutes.Login);
            }
            else
                Snackbar.Add(result.PrincipalMessage ?? "Erro ao registrar um novo usuário.", Severity.Error);
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