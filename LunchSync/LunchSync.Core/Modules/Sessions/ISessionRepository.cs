using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;
namespace LunchSync.Core.Modules.Sessions;

public interface ISessionRepository
{
    Task<Guid> SaveSessionAsync(Session session);
    Task<Session?> GetSessionByIdAsync(Guid sessionId, CancellationToken ct = default);
    Task<Session?> GetLastSessionByHostIdAsync(Guid hostId, CancellationToken ct = default);
}
