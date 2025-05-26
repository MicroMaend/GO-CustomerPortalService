using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var authToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(authToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = ParseClaimsFromJwt(authToken);
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            return new AuthenticationState(principal);
        }
        catch (Exception)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public void MarkUserAsAuthenticated(string authToken)
    {
        var claims = ParseClaimsFromJwt(authToken);
        var identity = new ClaimsIdentity(claims, "jwt");
        var authenticatedUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
    }

    public void MarkUserAsLoggedOut()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                var key = keyValuePair.Key;
                var value = keyValuePair.Value;

                if (value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.Array)
                    {
                        var values = element.EnumerateArray().Select(x => x.GetString());
                        claims.AddRange(values.Select(x => new Claim(key, x)));
                    }
                    else if (element.ValueKind == JsonValueKind.String)
                    {
                        claims.Add(new Claim(key, element.GetString()));
                    }
                    else if (element.ValueKind == JsonValueKind.Number)
                    {
                        claims.Add(new Claim(key, element.GetRawText()));
                    }
                    else if (element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
                    {
                        claims.Add(new Claim(key, element.GetRawText().ToLowerInvariant()));
                    }
                }
                else if (value != null)
                {
                    claims.Add(new Claim(key, value.ToString()));
                }
            }
        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}