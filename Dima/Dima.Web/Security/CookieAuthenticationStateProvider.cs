using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvider(
    IHttpClientFactory clientFactory
) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private bool _isAuthenticated = false;
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();

        return _isAuthenticated;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }

    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email) // Name é o identificador do usuário que estamos passando o email no caso
        };

        return claims;
    }

    public void NotifyAuthenticationStateChanged()
        => base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}