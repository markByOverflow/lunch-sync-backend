using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.Auth.Interfaces;

namespace LunchSync.Core.Modules.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ICognitoOAuthProvider _cognitoOAuthProvider;

    public AuthService(
        IUserRepository userRepository,
        ICognitoOAuthProvider cognitoOAuthProvider)
    {
        _userRepository = userRepository;
        _cognitoOAuthProvider = cognitoOAuthProvider;
    }

    public async Task<AuthTokenResponse> ExchangeCodeAsync(
        AuthCallbackRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateExchangeCodeRequest(request);

        var tokenResponse = await _cognitoOAuthProvider.ExchangeCodeAsync(request, cancellationToken);
        var profile = await _cognitoOAuthProvider.GetUserProfileAsync(tokenResponse.AccessToken, cancellationToken);
        var localUser = await SyncLocalUserAsync(profile, cancellationToken);

        EnsureUserIsActive(localUser);

        return tokenResponse.ToAuthTokenResponse(localUser);
    }

    public async Task<AuthTokenResponse> RefreshTokenAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRefreshTokenRequest(request);

        var tokenResponse = await _cognitoOAuthProvider.RefreshTokenAsync(request, cancellationToken);
        var profile = await _cognitoOAuthProvider.GetUserProfileAsync(tokenResponse.AccessToken, cancellationToken);
        var localUser = await SyncLocalUserAsync(profile, cancellationToken);

        EnsureUserIsActive(localUser);

        var refreshToken = string.IsNullOrWhiteSpace(tokenResponse.RefreshToken)
            ? request.RefreshToken.Trim()
            : tokenResponse.RefreshToken;

        return (tokenResponse with { RefreshToken = refreshToken })
            .ToAuthTokenResponse(localUser);
    }

    public Task RevokeTokenAsync(
        RevokeTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRevokeTokenRequest(request);
        return _cognitoOAuthProvider.RevokeTokenAsync(request, cancellationToken);
    }

    private async Task<User> SyncLocalUserAsync(
        CognitoUserProfileResult profile,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = profile.Email.Trim().ToLowerInvariant();
        var normalizedFullName = profile.FullName?.Trim();

        var existingBySub = await _userRepository.GetByCognitoSubAsync(profile.CognitoSub, cancellationToken);
        if (existingBySub is not null)
        {
            return await SyncUserProfileAsync(existingBySub, normalizedEmail, normalizedFullName, cancellationToken);
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existingByEmail is not null)
        {
            if (!string.Equals(existingByEmail.CognitoSub, profile.CognitoSub, StringComparison.Ordinal))
            {
                throw new AuthIdentityConflictException(normalizedEmail);
            }

            return await SyncUserProfileAsync(existingByEmail, normalizedEmail, normalizedFullName, cancellationToken);
        }

        return await _userRepository.AddAsync(new User
        {
            CognitoSub = profile.CognitoSub,
            Email = normalizedEmail,
            FullName = normalizedFullName
        }, cancellationToken);
    }

    private async Task<User> SyncUserProfileAsync(
        User user,
        string email,
        string? fullName,
        CancellationToken cancellationToken)
    {
        var hasChanges =
            !string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)
            || !string.Equals(user.FullName, fullName, StringComparison.Ordinal);

        if (!hasChanges)
        {
            return user;
        }

        user.Email = email;
        user.FullName = fullName;

        return await _userRepository.UpdateAsync(user, cancellationToken);
    }

    private static void ValidateExchangeCodeRequest(AuthCallbackRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
        {
            throw new ValidationException(
                "Du lieu doi authorization code khong hop le.",
                new Dictionary<string, string> { ["code"] = "Authorization code la bat buoc." });
        }
    }

    private static void ValidateRefreshTokenRequest(RefreshTokenRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return;
        }

        throw new ValidationException(
            "Du lieu refresh token khong hop le.",
            new Dictionary<string, string> { ["refresh_token"] = "Refresh token la bat buoc." });
    }

    private static void ValidateRevokeTokenRequest(RevokeTokenRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return;
        }

        throw new ValidationException(
            "Du lieu revoke token khong hop le.",
            new Dictionary<string, string> { ["refresh_token"] = "Refresh token la bat buoc." });
    }

    private static void EnsureUserIsActive(User user)
    {
        if (!user.IsActive)
        {
            throw new InactiveUserException();
        }
    }
}

file static class AuthResponseMappings
{
    public static AuthTokenResponse ToAuthTokenResponse(this CognitoTokenResponse tokenResponse, User localUser)
    {
        return new AuthTokenResponse(
            tokenResponse.AccessToken,
            tokenResponse.IdToken,
            tokenResponse.RefreshToken,
            tokenResponse.TokenType,
            tokenResponse.ExpiresIn,
            new CurrentUserResponse(
                localUser.Id,
                localUser.CognitoSub,
                localUser.Email,
                localUser.FullName,
                localUser.Role.ToString().ToLowerInvariant(),
                localUser.IsActive));
    }
}
