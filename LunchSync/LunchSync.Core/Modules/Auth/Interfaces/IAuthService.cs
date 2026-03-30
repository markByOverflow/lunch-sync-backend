namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> HandleLoginAsync(string cognitoSub, string email, string? name);
}
