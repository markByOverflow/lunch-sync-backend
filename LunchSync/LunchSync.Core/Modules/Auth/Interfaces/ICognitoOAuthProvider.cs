namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface ICognitoOAuthProvider
{
    Task<CognitoTokenResponse> ExchangeCodeAsync(AuthCallbackRequest request, CancellationToken cancellationToken = default);
    Task<CognitoTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task RevokeTokenAsync(RevokeTokenRequest request, CancellationToken cancellationToken = default);
    Task<CognitoUserProfileResult> GetUserProfileAsync(string accessToken, CancellationToken cancellationToken = default);
}
