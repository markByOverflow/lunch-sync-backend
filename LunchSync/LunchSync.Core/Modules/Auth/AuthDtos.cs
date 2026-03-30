namespace LunchSync.Core.Modules.Auth;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string? FullName
);

public sealed record RegisterResponse(
    Guid UserId,
    string Email,
    string? FullName,
    string Role,
    string Message
);

public sealed record LoginRequest(
    string Email,
    string Password
);

public sealed record LoginResponse(
    string AccessToken,
    int ExpiresIn,
    Guid UserId,
    string Email,
    string? FullName,
    string Role
);

public sealed record CurrentActorResponse(
    string? UserId,
    string? Email,
    string? Name,
    string? ActorType,
    IReadOnlyList<string> Roles
);

public sealed record RegistrationStatusResponse(
    string CognitoSub,
    bool IsRegistered
);

public sealed record CognitoRegisterResult(
    string CognitoSub,
    string Email,
    string? FullName
);

public sealed record CognitoLoginResult(
    string AppBearerToken,
    int ExpiresIn,
    string CognitoSub,
    string Email,
    string? FullName
);

public sealed record GuestAccessTokenRequest(
    string Nickname
);

public sealed record GuestAccessTokenResponse(
    string Token,
    DateTime ExpiresAtUtc,
    string HeaderName
);
