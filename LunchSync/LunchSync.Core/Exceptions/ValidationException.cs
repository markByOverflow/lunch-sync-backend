using System.Net;

namespace LunchSync.Core.Exceptions;


/// Input validation failure. Carries a field → message dictionary

public class ValidationException : DomainException
{
    public IReadOnlyDictionary<string, string> Details { get; }

    public ValidationException(string message, Dictionary<string, string> details)
        : base(message, "VALIDATION_ERROR", HttpStatusCode.BadRequest)
    {
        Details = details;
    }
}
