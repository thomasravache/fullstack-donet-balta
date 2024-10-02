using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using MudBlazor.Services;
using Dima.Web.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddMudServices();

// cria um http cliente e adiciona funcionalidades como anexar cookies ja na requisição, ajuda na gestão do cookie
builder.Services.AddHttpClient(Configuration.HttpClientName, options =>
{
    options.BaseAddress = new Uri(Configuration.BackendUrl);
})
    .AddHttpMessageHandler<CookieHandler>(); // interceptar mensagens de ida e de vinda podendeo inspecionar os cookies a cada mensagem

await builder.Build().RunAsync();
