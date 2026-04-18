namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthTokenResponse> ExchangeCodeAsync(AuthCallbackRequest request, CancellationToken cancellationToken = default);
    Task<AuthTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task RevokeTokenAsync(RevokeTokenRequest request, CancellationToken cancellationToken = default);
}
