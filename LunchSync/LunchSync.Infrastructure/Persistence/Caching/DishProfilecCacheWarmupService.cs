using LunchSync.Core.Modules.VotingAndScoring;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace LunchSync.Infrastructure.Persistence.Caching;

/// <summary>
/// Triggers cache warm-up once on application startup.
/// Register as a hosted service in DI.
/// </summary>
public sealed class DishProfileCacheWarmupService : IHostedService
{
    private readonly IDishProfileCache _cache;
    private readonly ILogger<DishProfileCacheWarmupService> _logger;

    public DishProfileCacheWarmupService(
        IDishProfileCache cache,
        ILogger<DishProfileCacheWarmupService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _cache.WarmUpAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Non-fatal: service starts, cache will be empty — scoring will fail gracefully
            _logger.LogError(ex, "DishProfileCache warm-up failed.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
