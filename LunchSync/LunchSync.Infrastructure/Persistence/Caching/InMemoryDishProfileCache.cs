using LunchSync.Core.Modules.VotingAndScoring;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LunchSync.Infrastructure.Persistence.Caching;

/// <summary>
/// Singleton in-memory cache populated on startup via IHostedService warm-up.
/// Thread-safe: list is replaced atomically via Interlocked / volatile ref swap.
/// </summary>
public sealed class InMemoryDishProfileCache : IDishProfileCache
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InMemoryDishProfileCache> _logger;

    private volatile IReadOnlyList<(Guid Id, double[] Profile)> _cache
        = Array.Empty<(Guid, double[])>();

    public bool IsReady => _cache.Count > 0;

    public InMemoryDishProfileCache(
        IServiceScopeFactory scopeFactory,
        ILogger<InMemoryDishProfileCache> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public IReadOnlyList<(Guid Id, double[] Profile)> GetAll() => _cache;

    public async Task WarmUpAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Warming up DishProfileCache…");

        await using var scope = _scopeFactory.CreateAsyncScope();
        var repo = scope.ServiceProvider
            .GetRequiredService<LunchSync.Core.Modules.RestaurantsAndDishes.Repositories.IDishRepository>();

        var dishes = await repo.GetAllDishProfilesAsync(ct);

        _cache = dishes
            .Where(d => d.Profile is not null)
            .Select(d => (d.Id, new[]
            {
                d.Profile.Soupy,
                d.Profile.Temperature,
                d.Profile.Heaviness,
                d.Profile.FlavorIntensity,
                d.Profile.Spicy,
                d.Profile.TextureComplexity,
                d.Profile.TimeRequired,
                d.Profile.Novelty
            }))
            .ToList();

        _logger.LogInformation("DishProfileCache warmed: {Count} dishes loaded.", _cache.Count);
    }
}
