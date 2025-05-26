using System.Net;
using System.Net.Http.Json;
using GOCore;

public class UserHttpService : IUserService
{
    private readonly HttpClient _http;
    private const string baseUrl = "http://localhost:5001";

    public UserHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _http.GetFromJsonAsync<User>($"{baseUrl}/user/{userId}");
    }

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        return await _http.GetFromJsonAsync<User>($"{baseUrl}/user/name/{userName}");
    }

    public async Task<string> GetUserNameByIdAsync(string userId)
    {
        var response = await _http.GetAsync($"{baseUrl}/user/username/{userId}");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _http.GetFromJsonAsync<List<User>>($"{baseUrl}/user/all");
    }

    public async Task<HttpStatusCode> CreateUserAsync(User user)
    {
        var response = await _http.PostAsJsonAsync($"{baseUrl}/user/add", user);

        return response.StatusCode;
    }

    public async Task UpdateUserAsync(string userId, User user)
    {
        await _http.PutAsJsonAsync($"{baseUrl}/user/{userId}", user);
    }

    public async Task DeleteUserAsync(string userId)
    {
        await _http.DeleteAsync($"{baseUrl}/user/{userId}");
    }


}
