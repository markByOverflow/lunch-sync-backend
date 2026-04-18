namespace LunchSync.Core.Modules.VotingAndScoring;
/// <summary>
/// In-memory cache for dish profile vectors.
/// Populated on startup. Access is O(1) / ~0.01 ms.
/// </summary>
public interface IDishProfileCache
{
    /// <summary>Returns all (DishId, Profile[8]) pairs loaded in memory.</summary>
    IReadOnlyList<(Guid Id, double[] Profile)> GetAll();

    /// <summary>True if the cache has been populated.</summary>
    bool IsReady { get; }

    /// <summary>Reload from the database (called on startup or admin refresh).</summary>
    Task WarmUpAsync(CancellationToken ct = default);
}
