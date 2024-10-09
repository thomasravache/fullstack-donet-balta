using System.Net.Http.Json;
using System.Text;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client
            .PostAsJsonAsync("api/v1/identity/login?useCookies=true", request);

        return result.IsSuccessStatusCode
            ? Response<string>.Success("Login Realizado com Sucesso", "Login Realizado com Sucesso")
            : Response<string>.Failure("Usuário ou senha inválidos");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

        await _client
            .PostAsJsonAsync("api/v1/identity/logout", emptyContent);
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client
            .PostAsJsonAsync("api/v1/identity/register", request);

        return result.IsSuccessStatusCode
            ? Response<string>.Success("Cadastro Realizado com Sucesso", "Cadastro Realizado com Sucesso")
            : Response<string>.Failure("Usuário ou senha inválidos");
    }
}