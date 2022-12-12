using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineRegistration.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(a => new HttpClient { BaseAddress = new Uri("https://localhost:7033/") }) ;

builder.Services.AddBlazorStrap();

await builder.Build().RunAsync();
