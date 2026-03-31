using System.Text.Json;

using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.ValueObjects;

namespace LunchSync.Core.Modules.Sessions;

public interface ISessionCache
{
    Task<Session?> GetActiveSessionByPinAsync(string pin, CancellationToken ct = default);
    // Set / invalidate
    Task SetSessionAsync(Session session, CancellationToken ct = default);
    Task RemoveSessionAsync(string pin, CancellationToken ct = default);
}
