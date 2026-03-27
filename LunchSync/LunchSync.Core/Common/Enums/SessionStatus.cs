namespace LunchSync.Core.Common.Enums
{
    public enum SessionStatus
    {
        Waiting = 0,    // Chờ người tham gia
        Voting = 1,     // Đang vote
        Results = 2,    // Hiển thị kết quả
        Picking = 3,    // Boom/Pick
        Done = 4        // Hoàn thành
    }
}
