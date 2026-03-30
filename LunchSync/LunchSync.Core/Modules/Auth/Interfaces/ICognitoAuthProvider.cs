namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface ICognitoAuthProvider
{
    Task<string> LoginAsync(string email, string password);
    Task<string> RegisterAsync(string email, string password, string? name);
}
