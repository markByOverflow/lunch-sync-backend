using System.Net;

namespace LunchSync.Core.Exceptions;
//403
public abstract class ForbiddenException : DomainException
{
    protected ForbiddenException(string message, string errorCode)
        : base(message, errorCode, HttpStatusCode.Forbidden) { }
}

public class NotHostException : ForbiddenException
{
    public NotHostException()
        : base("Chỉ host mới có thể thực hiện thao tác này", "NOT_HOST") { }
}

///Valid JWT but insufficient role
public class InsufficientRoleException : ForbiddenException
{
    public InsufficientRoleException(string requiredRole)
        : base($"Yêu cầu quyền '{requiredRole}' để thực hiện thao tác này", "FORBIDDEN") { }
}
