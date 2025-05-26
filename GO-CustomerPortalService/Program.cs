using GO_CustomerPortalService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuctionService, AuctionHttpService>();
builder.Services.AddScoped<IBiddingService, BiddingHttpService>();
builder.Services.AddScoped<ICatalogService, CatalogHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();

// Tilføj AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore(); // Tilføj authorization services

await builder.Build().RunAsync();