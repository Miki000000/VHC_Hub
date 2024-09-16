using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VHC_Erp.frontend;
using VHC_Erp.frontend.Configurations;
using VHC_Erp.frontend.Features.User;
using VHC_Erp.frontend.Utils.GlobalDialogErrorHandler;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddHttpClient(HttpClientConfigs.ClientName, config =>
{
    config.BaseAddress = new Uri(HttpClientConfigs.ServerRoute);
});

builder.Services.AddTransient<RegisterUserAsync>();
builder.Services.AddScoped<IGenerateDialog, GenerateDialog> ();


await builder.Build().RunAsync();