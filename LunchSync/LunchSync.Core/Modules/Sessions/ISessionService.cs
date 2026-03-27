using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Modules.Sessions;

public interface ISessionService
{
    Task<CreateSessionRes> CreateSessionAsync(CreateSessionReq request, Guid HostId);
    Task<JoinRes> JoinSessionAsync(string pin, JoinReq request);
    Task<SessionStartRes> StartSessionAsync(string pin, Guid hostId);
    Task CancelSessionAsync(string pin, Guid hostId);
    Task<Session?> GetSessionAsync(string pin);
    Task<Session?> GetSessionHistoryAsync(Guid sessionId);
}
