using System.Net;

namespace LunchSync.Core.Exceptions;

///409
public abstract class ConflictException : DomainException
{
    protected ConflictException(string message, string errorCode)
        : base(message, errorCode, HttpStatusCode.Conflict) { }
}

public class SessionFullException : ConflictException
{
    public SessionFullException()
        : base("Phiên đã đủ 8 người tham gia", "SESSION_FULL") { }
}

public class SessionAlreadyStartedException : ConflictException
{
    public SessionAlreadyStartedException()
        : base("Phiên đã bắt đầu bình chọn, không thể tham gia", "SESSION_ALREADY_STARTED") { }
}

public class NicknameTakenException : ConflictException
{
    public NicknameTakenException(string nickname)
        : base("Biệt danh đã được sử dụng trong phiên này", "NICKNAME_TAKEN") { }
}

public class AlreadyVotedException : ConflictException
{
    public AlreadyVotedException()
        : base("Bạn đã bình chọn trong phiên này rồi", "ALREADY_VOTED") { }
}

public class SubmissionAlreadyReviewedException : ConflictException
{
    public SubmissionAlreadyReviewedException(Guid submissionId)
        : base("Submission đã được duyệt trước đó", "SUBMISSION_ALREADY_REVIEWED") { }
}
