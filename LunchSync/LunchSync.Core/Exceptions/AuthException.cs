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
        : base("Email hoac mat khau khong dung.", "INVALID_CREDENTIALS")
    {
    }
}

public sealed class InactiveUserException : ForbiddenException
{
    public InactiveUserException()
        : base("Tai khoan hien dang bi vo hieu hoa.", "USER_INACTIVE")
    {
    }
}

public sealed class AuthIdentityConflictException : ConflictException
{
    public AuthIdentityConflictException(string email)
        : base($"Email '{email}' dang duoc lien ket voi mot tai khoan khac.", "AUTH_IDENTITY_CONFLICT")
    {
    }
}

public sealed class AuthProviderUnavailableException : DomainException
{
    public AuthProviderUnavailableException(string provider, Exception innerException)
        : base(
            message: $"Khong the ket noi den {provider}.",
            errorCode: "AUTH_PROVIDER_UNAVAILABLE",
            statusCode: HttpStatusCode.BadGateway,
            innerException: innerException)
    {
    }
}
