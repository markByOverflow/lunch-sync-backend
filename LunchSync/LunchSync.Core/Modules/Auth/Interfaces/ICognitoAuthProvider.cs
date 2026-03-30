namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface ICognitoAuthProvider
{
    Task<CognitoRegisterResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<CognitoLoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
