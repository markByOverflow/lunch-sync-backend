using System.Text.Json;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using StackExchange.Redis;

namespace LunchSync.Infrastructure.Auth;

public sealed class PendingRegistrationStore : IPendingRegistrationStore
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(30);

    private readonly IDatabase _database;

    public PendingRegistrationStore(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public Task SaveAsync(CognitoRegisterResult registration, CancellationToken cancellationToken = default)
    {
        var key = BuildKey(registration.Email);
        var payload = JsonSerializer.Serialize(registration, JsonOptions);
        return _database.StringSetAsync(key, payload, DefaultTtl);
    }

    public async Task<CognitoRegisterResult?> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(BuildKey(email));
        if (value.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<CognitoRegisterResult>(value!, JsonOptions);
    }

    public Task RemoveAsync(string email, CancellationToken cancellationToken = default)
    {
        return _database.KeyDeleteAsync(BuildKey(email));
    }

    private static string BuildKey(string email)
    {
        return $"auth:pending-registration:{email.Trim().ToLowerInvariant()}";
    }
}
