using GO_CustomerPortalService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuctionService, AuctionHttpService>();
builder.Services.AddScoped<IBiddingService, BiddingHttpService>();
builder.Services.AddScoped<ICatalogService, CatalogHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();

builder.Services.AddAuthorizationCore();

// Konfigurer autentificering ved hjælp af OidcAuthentication
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "/";
    options.ProviderOptions.MetadataUrl = "_configuration/.well-known/openid-configuration";
    options.ProviderOptions.ClientId = "blazorapp";
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.DefaultScopes.Clear();
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");

    options.AuthenticationPaths.LogInPath = "/login";
    options.AuthenticationPaths.LogInCallbackPath = "/authentication/login-callback";
    options.AuthenticationPaths.LogOutPath = "/logout";
    options.AuthenticationPaths.LogOutCallbackPath = "/authentication/logout-callback";
    options.AuthenticationPaths.RegisterPath = "/register";

});

await builder.Build().RunAsync();