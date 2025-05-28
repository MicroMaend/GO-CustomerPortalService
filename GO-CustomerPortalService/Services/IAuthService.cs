using GOCore;

public interface IAuthService
{
    Task<string?> LoginAsync(string userName, string password);
}