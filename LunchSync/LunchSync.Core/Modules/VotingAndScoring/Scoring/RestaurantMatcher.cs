using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.VotingAndScoring.Config;

namespace LunchSync.Core.Modules.VotingAndScoring.Scoring;
/// <summary>
/// Scores candidate restaurants against the top-7 scored dishes.
/// Pure, stateless. Registered as Singleton.
/// </summary>
public sealed class RestaurantMatcher
{
    // ── 6.6 Restaurant Matching ───────────────────────────────────────────────

    /// <summary>
    /// Score restaurants and return top-5 sorted by composite score.
    /// </summary>
    /// <param name="candidates">
    ///   Restaurants already filtered: in collection, serves ≥1 dish in top-7.
    /// </param>
    /// <param name="top7DishIds">Ranked list (index 0 = rank 1 = best).</param>
    /// <param name="sessionPriceTier">Tier selected by host for this session.</param>
    public IReadOnlyList<ScoredRestaurant> MatchTopRestaurants(
        IEnumerable<RestaurantCandidate> candidates,
        IReadOnlyList<Guid> top7DishIds,
        PriceTier sessionPriceTier)
    {
        var top7Set = top7DishIds
            .Select((id, idx) => (id, rank: idx + 1))   // rank 1..7
            .ToDictionary(x => x.id, x => x.rank);

        var scored = candidates.Select(r =>
        {
            double coverage = ComputeCoverage(r.ServedDishIds, top7Set);
            double priceFit = ComputePriceFit(r.PriceTier, sessionPriceTier);
            double ratingNorm = r.Rating.HasValue
                ? (r.Rating.Value - 1.0) / 4.0
                : ScoringConstants.DefaultRatingNorm;

            double score =
                ScoringConstants.WeightCoverage * coverage +
                ScoringConstants.WeightPriceFit * priceFit +
                ScoringConstants.WeightRating * ratingNorm;

            return new ScoredRestaurant(r.RestaurantId, score);
        })
        .OrderByDescending(x => x.Score)
        .Take(ScoringConstants.TopRestaurantsTotal)
        .ToList();

        return scored;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    /// <summary>
    /// coverage = Σ (8 - rank) / 28  for each dish served that's in top7
    /// </summary>
    private static double ComputeCoverage(
        IEnumerable<Guid> servedDishIds,
        Dictionary<Guid, int> top7RankMap)
    {
        double sum = servedDishIds
            .Where(top7RankMap.ContainsKey)
            .Sum(id => 8 - top7RankMap[id]);          // rank 1 → 7 pts, rank 7 → 1 pt

        return sum / ScoringConstants.CoverageDenominator;
    }

    private static double ComputePriceFit(PriceTier restaurantTier, PriceTier sessionTier)
    {
        int diff = Math.Abs((int)restaurantTier - (int)sessionTier);
        return diff switch
        {
            0 => ScoringConstants.PriceFitSameTier,
            1 => ScoringConstants.PriceFitAdjacentTier,
            _ => ScoringConstants.PriceFitFarTier
        };
    }
}

// ── DTOs used only inside this layer ─────────────────────────────────────────

public sealed record RestaurantCandidate(
    Guid RestaurantId,
    PriceTier PriceTier,
    double? Rating,
    IReadOnlyList<Guid> ServedDishIds
);

public sealed record ScoredRestaurant(Guid RestaurantId, double Score);
