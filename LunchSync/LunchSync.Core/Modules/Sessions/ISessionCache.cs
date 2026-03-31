using System.Text.Json;

using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.ValueObjects;

namespace LunchSync.Core.Modules.Sessions;

public interface ISessionCache
{
    Task SaveActiveSessionAsync(Session session, int expireMinutes);
    Task<Session?> GetActiveSessionByPinAsync(string pin);
    Task UpdateStatusAndExpireAsync(string pin, SessionStatus newStatus, int expireMinutes);
    Task<List<Participant>> GetParticipantsAsync(string pin);
    Task RemoveSessionAsync(string pin);
    Task<int> TryJoinAtomicAsync(string pin, Participant participant, int maxParticipants, int expireMinutes);
}
