using System.Net;

namespace LunchSync.Core.Exceptions;

public abstract class AuthenticationException : DomainException
{
    protected AuthenticationException(
        string message,
        string errorCode,
        HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
        : base(message, errorCode, statusCode)
    {
    }
}

public sealed class InvalidCredentialsException : AuthenticationException
{
    public InvalidCredentialsException()
        : base("Email hoặc mật khẩu không đúng.", "INVALID_CREDENTIALS")
    {
    }
}

public sealed class InactiveUserException : ForbiddenException
{
    public InactiveUserException()
        : base("Tài khoản hiện đang bị vô hiệu hóa.", "USER_INACTIVE")
    {
    }
}

public sealed class AuthIdentityConflictException : ConflictException
{
    public AuthIdentityConflictException(string email)
        : base($"Email '{email}' đang được liên kết với một tài khoản khác.", "AUTH_IDENTITY_CONFLICT")
    {
    }
}
