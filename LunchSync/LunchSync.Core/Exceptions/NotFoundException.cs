using System.Net;

namespace LunchSync.Core.Exceptions;

public abstract class NotFoundException : DomainException
{
    protected NotFoundException(string message, string errorCode)
        : base(message, errorCode, HttpStatusCode.NotFound) { }
}

public class SessionNotFoundException : NotFoundException
{
    public SessionNotFoundException(string pin)
        : base($"Không tìm thấy phiên với mã PIN '{pin}'", "SESSION_NOT_FOUND") { }

}
public class SessionNotFoundByIdException : NotFoundException
{
    public SessionNotFoundByIdException(Guid sessionId)
        : base($"Không tìm thấy phiên với ID '{sessionId}'", "SESSION_NOT_FOUND") { }
}

public class DishNotFoundException : NotFoundException
{
    public DishNotFoundException(Guid dishId)
        : base("Không tìm thấy món ăn ", "DISH_NOT_FOUND") { }
}

public class CollectionNotFoundException : NotFoundException
{
    public CollectionNotFoundException(Guid collectionId)
        : base("Không tìm thấy bộ sưu tập ", "COLLECTION_NOT_FOUND") { }
}
public class RestaurantNotFoundException : NotFoundException
{
    public RestaurantNotFoundException(Guid restaurantId)
        : base("Không tìm thấy nhà hàng", "RESTAURANT_NOT_FOUND") { }
}
public class SubmissionNotFoundException : NotFoundException
{
    public SubmissionNotFoundException(Guid submissionId)
        : base("Không tìm thấy submission", "SUBMISSION_NOT_FOUND") { }
}
