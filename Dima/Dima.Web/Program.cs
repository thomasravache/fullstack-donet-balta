using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using MudBlazor.Services;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

// Se pedir um authenticationStateProvider na aplicação, mapear para a classe que criamos
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>(); // como é webassembly não precisa de preocupar tanto com o life cycle assim como no servidor
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

// cria um http cliente e adiciona funcionalidades como anexar cookies ja na requisição, ajuda na gestão do cookie
builder.Services.AddHttpClient(Configuration.HttpClientName, options =>
{
    options.BaseAddress = new Uri(Configuration.BackendUrl);
})
    .AddHttpMessageHandler<CookieHandler>(); // interceptar mensagens de ida e de vinda podendeo inspecionar os cookies a cada mensagem



await builder.Build().RunAsync();
