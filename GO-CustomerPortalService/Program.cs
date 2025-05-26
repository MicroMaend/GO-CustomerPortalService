using GO_CustomerPortalService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization; 
using GO_CustomerPortalService.Authentication;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuctionService, AuctionHttpService>();
builder.Services.AddScoped<IBiddingService, BiddingHttpService>();
builder.Services.AddScoped<ICatalogService, CatalogHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();

// Tilføj din CustomAuthStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

//thoughts??

// Tilføj AuthorizationCore
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();