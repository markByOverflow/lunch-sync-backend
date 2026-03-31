using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Common.Enums;

using StackExchange.Redis;

using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

namespace LunchSync.Infrastructure.Persistence.Caching;

public class SessionCache : ISessionCache
{
    private readonly IDatabase _db;

    public SessionCache(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SaveActiveSessionAsync(Session session, int expireMinutes)
    {
        var key = RedisKeyBuilder.Data(session.Pin);
        var participantKey = RedisKeyBuilder.Participants(session.Pin);
        var nameKey = RedisKeyBuilder.Names(session.Pin);

        //xóa set participants và names cũ
        await _db.KeyDeleteAsync(new RedisKey[] { participantKey, nameKey });

        var entries = new HashEntry[]
        {
        new("Id", session.Id.ToString()),
        new("HostId", session.HostId.ToString()),
        new("Status", (int)session.Status),
        new("CreatedAt", session.CreatedAt.ToString("O")),
        new("ExpiresAt", session.ExpiresAt?.ToString("O") ?? ""),
        new("CollectionId", session.CollectionId.ToString()),
        new("PriceTier", (int)session.PriceTier)
        };

        await _db.HashSetAsync(key, entries);

        // Set TTL cho tất cả các key liên quan
        var ttl = TimeSpan.FromMinutes(expireMinutes);
        await _db.KeyExpireAsync(key, ttl);
        await _db.KeyExpireAsync(participantKey, ttl);
        await _db.KeyExpireAsync(nameKey, ttl);
    }

    public async Task<List<Participant>> GetParticipantsAsync(string pin, CancellationToken ct = default)
    {
        var setKey = RedisKeyBuilder.Participants(pin);
        var members = await _db.SetMembersAsync(setKey); //lay participant trong set

        if (members == null || members.Length == 0)
        {
            return new List<Participant>();
        }
        return members
            .Select(m => JsonSerializer.Deserialize<Participant>(m!)) // duyet tung chuoi json trong redis va giai ma nguoc lai thanh participant
            .Where(x => x != null)
            .ToList()!;
    }

    public async Task<Session?> GetActiveSessionByPinAsync(string pin, CancellationToken ct = default)
    {
        var key = RedisKeyBuilder.Data(pin);
        var data = await _db.HashGetAllAsync(key);
        if (data.Length == 0)
        { return null; }

        var dict = data.ToDictionary(x => x.Name.ToString(), x => x.Value.ToString());

        return new Session
        {
            Id = dict.TryGetValue("Id", out var sId) ? Guid.Parse(sId) : Guid.Empty,
            Pin = pin,
            HostId = dict.TryGetValue("HostId", out var hId) ? Guid.Parse(hId) : Guid.Empty,
            Status = dict.TryGetValue("Status", out var st) ? Enum.Parse<SessionStatus>(st) : SessionStatus.Waiting,
            CreatedAt = dict.TryGetValue("CreatedAt", out var tc) ? DateTime.Parse(tc) : DateTime.UtcNow,
            ExpiresAt = dict.TryGetValue("ExpiresAt", out var te) ? DateTime.Parse(te) : DateTime.MinValue,
            CollectionId = dict.TryGetValue("CollectionId", out var cId) ? Guid.Parse(cId) : Guid.Empty,
            PriceTier = dict.TryGetValue("PriceTier", out var pt) ? Enum.Parse<PriceTier>(pt) : PriceTier.Under40k
        };
    }
    public async Task RemoveSessionAsync(string pin)
    {
        var keys = new RedisKey[]
        {
        RedisKeyBuilder.Names(pin),
        RedisKeyBuilder.Participants(pin),
        RedisKeyBuilder.Data(pin)
        };
        await _db.KeyDeleteAsync(keys);
    }
    public async Task<int> TryJoinAtomicAsync(string pin, Participant participant, int maxParticipants, int expireMinutes)
    {

        var participantJson = JsonSerializer.Serialize(participant);
        var nickname = participant.Nickname.ToLower().Trim();

        var keys = new RedisKey[]
        {
        RedisKeyBuilder.Names(pin), //KEYS[1]
        RedisKeyBuilder.Participants(pin), //KEYS[2]
        RedisKeyBuilder.Data(pin) //KEYS[3]
        };

        string luaScript = @"
            -- 1. Kiểm tra trạng thái phòng
            local status = redis.call('HGET', KEYS[3], 'Status')
            if status ~= '0' then return 3 end

            -- 2. check duplicated name
            if redis.call('SISMEMBER', KEYS[1], ARGV[1]) == 1 then return 1 end

            -- 3. Check count
            local currentCount = tonumber(redis.call('SCARD', KEYS[2]) or '0')
            if currentCount >= tonumber(ARGV[2]) then return 2  end

            -- 4. Add (Atomic)
            redis.call('SADD', KEYS[1], ARGV[1]) -- Thêm tên
            redis.call('SADD', KEYS[2], ARGV[3]) -- participant

            -- 5. Set TTL
            local ttl = tonumber(ARGV[4])
            for i, key in ipairs(KEYS) do
                redis.call('EXPIRE', key, ttl)
            end

            return 0 -- Thành công
        ";

        var result = await _db.ScriptEvaluateAsync(luaScript, keys,
            new RedisValue[] { nickname, maxParticipants, participantJson, expireMinutes * 60 });
        // ARG[1]   ARG[2]           ARG[3]           ARG[4]
        return (int)result;
    }

    public async Task UpdateStatusAndExpireAsync(string pin, SessionStatus newStatus, int expireMinutes)
    {
        var key = RedisKeyBuilder.Data(pin);
        var participantKey = RedisKeyBuilder.Participants(pin);
        var nameKey = RedisKeyBuilder.Names(pin);

        // 1. Cập nhật Status
        await _db.HashSetAsync(key, "Status", (int)newStatus);

        // 2. Gia hạn thời gian sống (TTL)
        var ttl = TimeSpan.FromMinutes(expireMinutes);
        await _db.KeyExpireAsync(key, ttl);
        await _db.KeyExpireAsync(participantKey, ttl);
        await _db.KeyExpireAsync(nameKey, ttl);
    }
}
