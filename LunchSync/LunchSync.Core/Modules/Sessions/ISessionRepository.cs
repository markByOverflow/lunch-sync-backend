using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;
namespace LunchSync.Core.Modules.Sessions;

public interface ISessionRepository
{
    Task<Guid> SaveHistoryAsync(Session session);
    Task<Session?> GetHistoryByIdAsync(Guid sessionId);
    Task<Session?> GetLastSessionByHostIdAsync(Guid hostId);
}
