using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthHttpService : IAuthService
{
    private readonly HttpClient _http;
    const string baseUrl = "http://localhost:5004";
    //private const string baseUrl = "/api/auth";

    public AuthHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> LoginAsync(string userName, string password)
    {
        var loginRequest = new { UserName = userName, Password = password };
        var response = await _http.PostAsJsonAsync($"{baseUrl}/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // Håndter uautoriseret adgang, f.eks. ved at returnere null eller en specifik besked
            return null;
        }
        else
        {
            // Håndter andre fejl
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error during login: {response.StatusCode} - {errorContent}");
            return null;
        }
    }
}

// Definer en klasse til at deserialisere login-responset (som indeholder token)
public class LoginResponse
{
    public string? Token { get; set; }
}