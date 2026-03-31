using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;

using StackExchange.Redis;

using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

namespace LunchSync.Infrastructure.Persistence.Caching;

public class SessionCache : ISessionCache
{
    private readonly IDistributedCache _cache;

    private static readonly DistributedCacheEntryOptions _waitingOpts = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)  // session expire sau 15p, buffer thêm
    };
    private static readonly DistributedCacheEntryOptions _activeOpts = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(90)  // voting + results + picking
    };

    public SessionCache(IDistributedCache cache)
    {
        _cache = cache;
    }
    private static string Key(string pin) => $"session:{pin}";

    public async Task SetSessionAsync(Session session, CancellationToken ct = default)
    {
        var opts = session.Status == SessionStatus.Waiting ? _waitingOpts : _activeOpts;
        var json = JsonSerializer.Serialize(session);
        await _cache.SetStringAsync(Key(session.Pin), json, opts, ct);
    }

    public async Task<Session?> GetActiveSessionByPinAsync(string pin, CancellationToken ct = default)
    {
        var json = await _cache.GetStringAsync(Key(pin), ct);
        return json == null ? null : JsonSerializer.Deserialize<Session>(json);
    }

    public async Task RemoveSessionAsync(string pin, CancellationToken ct = default)
    {
        await _cache.RemoveAsync(Key(pin), ct);
    }
}
