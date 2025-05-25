using System.Net;
using GOCore;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByNameAsync(string userName);
    Task<List<User>> GetAllUsersAsync();
    Task<HttpStatusCode> CreateUserAsync(User user);
    Task UpdateUserAsync(string userId, User user);
    Task DeleteUserAsync(string userId);
}
