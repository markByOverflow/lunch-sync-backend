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

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entity, object id)
        : base(
            message: $"{entity} with id '{id}' was not found",
            errorCode: $"{entity.ToUpper()}_NOT_FOUND",
            statusCode: HttpStatusCode.NotFound)
    { }
}

public class BusinessRuleViolationException : DomainException
{
    public BusinessRuleViolationException(string rule)
        : base(
            message: $"Business rule violated: {rule}",
            errorCode: "BUSINESS_RULE_VIOLATION",
            statusCode: HttpStatusCode.UnprocessableEntity)
    { }
}

public class DuplicateEntityException : DomainException
{
    public DuplicateEntityException(string entity, string field, object value)
        : base(
            message: $"{entity} with {field} '{value}' already exists",
            errorCode: $"{entity.ToUpperInvariant()}_DUPLICATE",
            statusCode: HttpStatusCode.Conflict)
    { }
}
