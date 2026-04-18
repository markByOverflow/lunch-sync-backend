using System.Net;

namespace LunchSync.Core.Exceptions;
//400/410/422
//400
public abstract class InvalidStateException : DomainException
{
    protected InvalidStateException(string message)
        : base(message, "INVALID_STATE", HttpStatusCode.BadRequest) { }
}
public class InsufficientParticipantsException : DomainException
{
    public InsufficientParticipantsException(int current, int required = 3)
        : base(
            $"Cần ít nhất {required} người tham gia để bắt đầu (hiện tại: {current})",
            "INSUFFICIENT_PARTICIPANTS",
            HttpStatusCode.BadRequest)
    { }
}

public class InvalidPickException : DomainException
{
    public InvalidPickException(Guid restaurantId)
        : base(
            "Nhà hàng không thuộc top 3 còn lại",
            "INVALID_PICK",
            HttpStatusCode.BadRequest)
    { }
}
//422
public class NoRestaurantsMatchException : DomainException
{
    public NoRestaurantsMatchException()
        : base(
            "Không có nhà hàng nào phù hợp với tiêu chí bình chọn",
            "NO_RESTAURANTS_MATCH",
            HttpStatusCode.UnprocessableEntity)
    { }
}
//410
public class SessionExpiredException : DomainException
{
    public SessionExpiredException()
        : base("Phiên đã hết hạn", "SESSION_EXPIRED", HttpStatusCode.Gone) { }
}

public class VotingTimeoutException : DomainException
{
    public VotingTimeoutException()
        : base("Thời gian bình chọn đã kết thúc (vượt quá 120 giây)", "VOTING_TIMEOUT", HttpStatusCode.Gone) { }
}

public class ResultsNotReadyException : InvalidStateException
{
    public ResultsNotReadyException(string message)
        :base( message)
    {
    }
}

public class BoomNotReadyException : InvalidStateException
{
    public BoomNotReadyException(string message)
        :base( message)
    {
    }
}

public class PickNotReadyException : InvalidStateException
{
    public PickNotReadyException(string message)
        :base( message)
    {
    }
}

public class VoteNotReadyException : InvalidStateException
{
    public VoteNotReadyException(string message)
        :base( message)
    {
    }
}
