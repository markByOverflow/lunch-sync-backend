using System.Text.Json.Serialization;

namespace LunchSync.Core.Modules.Auth;

public sealed record AuthCallbackRequest(
    [property: JsonPropertyName("code")]
    string Code
);

public sealed record RefreshTokenRequest(
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken
);

public sealed record RevokeTokenRequest(
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken
);

public sealed record AuthTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("id_token")] string? IdToken,
    [property: JsonPropertyName("refresh_token")] string? RefreshToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("user")] CurrentUserResponse User
);

public sealed record CurrentUserResponse(
    Guid? UserId,
    string? CognitoSub,
    string? Email,
    string? FullName,
    string? Role,
    bool IsActive
);

public sealed record CognitoTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("id_token")] string? IdToken,
    [property: JsonPropertyName("refresh_token")] string? RefreshToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("scope")] string? Scope
);

public sealed record CognitoUserProfileResult(
    [property: JsonPropertyName("sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("name")] string? FullName
);
