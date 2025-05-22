using GO_CustomerPortalService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("/api/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuctionService, AuctionHttpService>();


await builder.Build().RunAsync();
