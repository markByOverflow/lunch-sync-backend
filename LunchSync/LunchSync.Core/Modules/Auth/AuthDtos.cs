using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LunchSync.Core.Modules.Auth;

public sealed record RegisterRequest(
    [property: Required, EmailAddress, JsonPropertyName("email")] string Email,

    [property: Required, MinLength(6), JsonPropertyName("password")] string Password,

    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record RegisterResponse(
    [property: JsonPropertyName("user_id")] Guid UserId,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("message")] string Message
);

public sealed record LoginRequest(
    [property: Required, EmailAddress, JsonPropertyName("email")] string Email,

    [property: Required, MinLength(6), JsonPropertyName("password")] string Password
);

public sealed record LoginResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("user_id")] Guid UserId,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName,
    [property: JsonPropertyName("role")] string Role
);

public sealed record CurrentUserResponse(
    Guid? UserId,
    string? CognitoSub,
    string? Email,
    string? FullName,
    string? Role,
    bool IsActive
);

public sealed record CognitoRegisterResult(
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record CognitoLoginResult(
    string AccessToken,
    int ExpiresIn,
    string CognitoSub,
    string Email,
    string? FullName
);
