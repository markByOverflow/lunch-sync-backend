using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Modules.Sessions;

public interface ISessionService
{
    Task<CreateSessionRes> CreateSessionAsync(CreateSessionReq request, Guid HostId);
    Task<JoinRes> JoinSessionAsync(Guid? userId, string pin, JoinReq request, CancellationToken ct = default);
    Task<SessionStartRes> StartSessionAsync(string pin, Guid hostId, CancellationToken ct = default);
    Task CancelSessionAsync(string pin, Guid hostId, CancellationToken ct = default);
    Task<Session?> GetSessionAsync(string pin, CancellationToken ct = default);
    Task<Session?> GetSessionHistoryAsync(Guid sessionId, CancellationToken ct = default);
}
