using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LunchSync.Core.Modules.Auth;

public sealed record RegisterRequest(
    [param: Required(ErrorMessage = "Email la bat buoc."), EmailAddress(ErrorMessage = "Email khong dung dinh dang.")]
    [property: JsonPropertyName("email")] string Email,

    [param: Required(ErrorMessage = "Mat khau la bat buoc."), MinLength(6, ErrorMessage = "Mat khau phai co it nhat 6 ky tu.")]
    [property: JsonPropertyName("password")] string Password,

    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record RegisterResponse(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName,
    [property: JsonPropertyName("message")] string Message
);

public sealed record VerifyOtpRequest(
    [param: Required(ErrorMessage = "Email la bat buoc."), EmailAddress(ErrorMessage = "Email khong dung dinh dang.")]
    [property: JsonPropertyName("email")] string Email,
    [param: Required(ErrorMessage = "Ma OTP la bat buoc."), MinLength(6, ErrorMessage = "Ma OTP phai co it nhat 6 ky tu.")]
    [property: JsonPropertyName("otp")] string Otp
);

public sealed record VerifyOtpResponse(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("message")] string Message
);

public sealed record ResendOtpRequest(
    [param: Required(ErrorMessage = "Email la bat buoc."), EmailAddress(ErrorMessage = "Email khong dung dinh dang.")]
    [property: JsonPropertyName("email")] string Email
);

public sealed record ResendOtpResponse(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("message")] string Message
);

public sealed record LoginRequest(
    [param: Required(ErrorMessage = "Email la bat buoc."), EmailAddress(ErrorMessage = "Email khong dung dinh dang.")]
    [property: JsonPropertyName("email")] string Email,

    [param: Required(ErrorMessage = "Mat khau la bat buoc."), MinLength(6, ErrorMessage = "Mat khau phai co it nhat 6 ky tu.")]
    [property: JsonPropertyName("password")] string Password
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
    [property: JsonPropertyName("user_id")] Guid? UserId,
    [property: JsonPropertyName("cognito_sub")] string? CognitoSub,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("full_name")] string? FullName,
    [property: JsonPropertyName("role")] string? Role,
    [property: JsonPropertyName("is_active")] bool IsActive
);

public sealed record CognitoRegisterResult(
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record CognitoLoginResult(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);

public sealed record CognitoUserProfileResult(
    [property: JsonPropertyName("cognito_sub")] string CognitoSub,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string? FullName
);
