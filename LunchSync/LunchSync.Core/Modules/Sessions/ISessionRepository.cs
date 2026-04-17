using LunchSync.Core.Modules.Sessions.Entities;


using System.Linq.Expressions;
namespace LunchSync.Core.Modules.Sessions;

public interface ISessionRepository
{
    Task<Guid> SaveSessionAsync(Session session);
    Task UpdateSessionAsync<TProperty>(Session session, Expression<Func<Session, TProperty>> propertyExpression, TProperty value);

    // Read active (dùng cho flow đang chạy)
    Task<Session?> GetActiveSessionByPinAsync(string pin, CancellationToken ct = default);
    Task SaveParticipantAsync(Participant participant);

    // Read history (đã done/cancelled)
    Task<Session?> GetSessionByIdAsync(Guid sessionId, CancellationToken ct = default);
    Task<Session?> GetLastSessionByHostIdAsync(Guid hostId, CancellationToken ct = default);

    /// <summary>
    /// Persist a participant's vote result: PrefVector + VotedAt only.
    /// Participant is attached and only those two columns are marked Modified.
    /// </summary>
    Task UpdateParticipantVoteAsync(Participant participant, CancellationToken ct = default);
}
