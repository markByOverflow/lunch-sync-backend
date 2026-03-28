using System.Net;

namespace LunchSync.Core.Exceptions;

public abstract class DomainException : Exception
{
    public string ErrorCode { get; }
    public HttpStatusCode StatusCode { get; }  // HTTP status mapping

    protected DomainException(
        string message,
        string errorCode,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    protected DomainException(
       string message,
       string errorCode,
       HttpStatusCode statusCode,
       Exception innerException)
       : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}

// Concrete exceptions
public class BusinessRuleViolationException : DomainException
{
    public BusinessRuleViolationException(string rule)
        : base(
            message: $"Business rule violated: {rule}",
            errorCode: "BUSINESS_RULE_VIOLATION",
            statusCode: HttpStatusCode.UnprocessableEntity)
    { }
}
