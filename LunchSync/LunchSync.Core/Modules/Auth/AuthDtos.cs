using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LunchSync.Core.Modules.Auth;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string? FullName
);

public sealed record RegisterResponse(
    string Email,
    string? FullName,
    string Message
);

public sealed record VerifyOtpRequest(
    string Email,
    string Otp
);

public sealed record VerifyOtpResponse(
    string Email,
    string Message
);

public sealed record ResendOtpRequest(
    string Email
);

public sealed record ResendOtpResponse(
    string Email,
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
    string CognitoSub,string Email,string? FullName
);

public sealed record CognitoLoginResult(
    string AccessToken,
    int ExpiresIn,
    string CognitoSub,
    string Email,
    string? FullName
);
