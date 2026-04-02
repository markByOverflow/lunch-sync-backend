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

public sealed record CurrentUserResponse(
    Guid? UserId,
    string? CognitoSub,
    string? Email,
    string? FullName,
    string? Role,
    bool IsActive
);

public sealed record CognitoRegisterResult(
    string CognitoSub,
    string Email,
    string? FullName
);

public sealed record CognitoLoginResult(
    string AccessToken,
    int ExpiresIn,
    string CognitoSub,
    string Email,
    string? FullName
);
