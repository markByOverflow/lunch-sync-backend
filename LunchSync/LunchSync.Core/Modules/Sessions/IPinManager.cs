namespace LunchSync.Core.Modules.Sessions;

public interface IPinManager
{
    // Lấy PIN từ Pool hoặc Gen mới nếu Pool trống
    Task<string> GetUnusedPinAsync(Guid sessionId);

    // Đưa PIN trở lại Pool khi Session kết thúc
    Task<bool> ReleasePinAsync(string pin);
}
