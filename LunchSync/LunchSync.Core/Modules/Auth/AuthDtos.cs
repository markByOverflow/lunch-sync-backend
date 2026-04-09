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

public sealed record CurrentActorResponse(
    [property: JsonPropertyName("user_id")] string? UserId,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("actor_type")] string? ActorType,
    [property: JsonPropertyName("roles")] IReadOnlyList<string> Roles
);

public sealed record RegistrationStatusResponse(
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("is_registered")] bool IsRegistered
);

public sealed record CognitoRegisterResult(
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record CognitoLoginResult(
    [property: JsonPropertyName("app_bearer_token")] string AppBearerToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record GuestAccessTokenRequest(
    [property: Required, JsonPropertyName("nickname")] string Nickname
);

public sealed record GuestAccessTokenResponse(
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("expires_at_utc")] DateTime ExpiresAtUtc,
    [property: JsonPropertyName("header_name")] string HeaderName
);
