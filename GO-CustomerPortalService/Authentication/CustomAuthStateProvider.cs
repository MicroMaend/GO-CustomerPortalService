using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // Anonym bruger
        }

        return await CreateAuthenticationState(token);
    }

    private async Task<AuthenticationState> CreateAuthenticationState(string token)
    {
        try
        {
            var claimsPrincipal = ParseClaimsPrincipal(token);
            if (claimsPrincipal != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return new AuthenticationState(claimsPrincipal);
            }
        }
        catch (Exception)
        {
            // Log eventuelt fejl ved parsing af token
            await _localStorage.RemoveItemAsync("authToken");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // Anonym ved fejl
        }

        await _localStorage.RemoveItemAsync("authToken");
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // Anonym hvis parsing fejler
    }

    private ClaimsPrincipal ParseClaimsPrincipal(string token)
{
    try
    {
        var parts = token.Split('.');
        if (parts.Length < 2)
        {
            return null; // Ugyldigt token format
        }
        var payloadBase64 = parts[1].Replace('-', '+').Replace('_', '/');
        while (payloadBase64.Length % 4 != 0)
        {
            payloadBase64 += '=';
        }
        var jsonPayload = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(payloadBase64));
        var claimsFromToken = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonPayload);

        var claims = new List<Claim>();
        if (claimsFromToken != null)
        {
            foreach (var keyValuePair in claimsFromToken)
            {
                claims.Add(new Claim(keyValuePair.Key, keyValuePair.Value?.ToString()));
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        }
    }
    catch (Exception)
    {
        // Log eventuelt fejl ved parsing
        return null;
    }

    return null;
}

    public async Task MarkUserAsAuthenticated(string token)
    {
        var authenticationState = await CreateAuthenticationState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authToken");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}