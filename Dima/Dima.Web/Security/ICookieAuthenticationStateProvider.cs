using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync(); // Verifica se o usuário está ou não autenticado
    Task<AuthenticationState> GetAuthenticationStateAsync(); // Obtém o usuário autenticado
    void NotifyAuthenticationStateChanged(); // Notificar mudanças de autenticação
}