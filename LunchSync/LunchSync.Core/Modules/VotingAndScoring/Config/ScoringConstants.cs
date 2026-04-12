using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSync.Core.Modules.VotingAndScoring.Config;

public static class ScoringConstants
{
    // ── Group aggregation 
    /// <summary>Spicy dimension threshold: if any participant's spicy &lt; this, veto kicks in.</summary>
    public const double SpicyVetoThreshold = -0.5;

    /// <summary>Effective spicy value applied after veto.</summary>
    public const double SpicyVetoCap = -0.3;

    // ── Restaurant scoring 
    public const double WeightCoverage = 0.5;
    public const double WeightPriceFit = 0.3;
    public const double WeightRating = 0.2;

    /// <summary>Coverage denominator for top-7: Σ(8-rank) for rank 1..7 = 7+6+5+4+3+2+1 = 28</summary>
    public const double CoverageDenominator = 28.0;

    public const double PriceFitSameTier = 1.0;
    public const double PriceFitAdjacentTier = 0.5;
    public const double PriceFitFarTier = 0.0;

    /// <summary>Fallback rating_norm when restaurant has no rating.</summary>
    public const double DefaultRatingNorm = 0.5;

    // ── Result sizes 
    public const int TopDishesForUi = 3;
    public const int TopDishesInternal = 7;
    public const int TopRestaurantsTotal = 5;
    public const int BoomEliminated = 2;   // random pick
    public const int PickingRemaining = 3;   // TopRestaurantsTotal - BoomEliminated
}
