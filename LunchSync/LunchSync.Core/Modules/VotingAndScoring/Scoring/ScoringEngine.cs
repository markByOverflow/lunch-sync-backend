using LunchSync.Core.Modules.VotingAndScoring.Config;

namespace LunchSync.Core.Modules.VotingAndScoring.Scoring;
/// <summary>
/// Pure, stateless scoring math. No I/O, no DI dependencies.
/// Registered as Singleton.
/// </summary>
public sealed class ScoringEngine
{
    // ── 6.2 Individual Vector 

    /// <summary>
    /// Build a normalized individual preference vector from 8 binary answers.
    /// </summary>
    /// <param name="choices">String of length 8, chars in {'A','B'}.</param>
    public double[] BuildIndividualVector(string choices)
    {
        ValidateChoices(choices);

        var raw = new double[BinaryChoiceDefinitions.DimensionCount];
        var bcs = BinaryChoiceDefinitions.All;

        for (int i = 0; i < bcs.Count; i++)
        {
            var impact = choices[i] == 'A' ? bcs[i].ImpactA : bcs[i].ImpactB;
            for (int d = 0; d < raw.Length; d++)
                raw[d] += impact[d];
        }

        // Normalize to [-1, +1]
        double maxAbs = raw.Max(Math.Abs);
        if (maxAbs > 0)
            for (int d = 0; d < raw.Length; d++)
                raw[d] /= maxAbs;

        return raw;
    }

    // ── 6.3 Group Aggregation + Veto 

    /// <summary>
    /// Aggregate N individual vectors into one effective group vector.
    /// Applies spicy veto if any participant strongly dislikes spice.
    /// </summary>
    public double[] AggregateGroupVector(IReadOnlyList<double[]> individualVectors)
    {
        if (individualVectors is null || individualVectors.Count == 0)
            throw new ArgumentException("At least one vector required.", nameof(individualVectors));

        int n = individualVectors.Count;
        int dims = BinaryChoiceDefinitions.DimensionCount;
        var effective = new double[dims];

        for (int d = 0; d < dims; d++)
        {
            double mean = individualVectors.Average(v => v[d]);
            double std = StdDev(individualVectors.Select(v => v[d]), mean);
            double consensus = Math.Max(0, 1 - std);
            effective[d] = mean * consensus;
        }

        // Spicy veto: if any user hates spice, cap the group's spicy
        bool anySpicyVeto = individualVectors.Any(
            v => v[BinaryChoiceDefinitions.Spicy] < ScoringConstants.SpicyVetoThreshold);

        if (anySpicyVeto)
            effective[BinaryChoiceDefinitions.Spicy] =
                Math.Min(effective[BinaryChoiceDefinitions.Spicy], ScoringConstants.SpicyVetoCap);

        return effective;
    }

    // ── 6.5 Dish Scoring 

    /// <summary>
    /// Score and rank dishes against the effective group vector.
    /// Returns all scored dishes sorted descending.
    /// </summary>
    public IReadOnlyList<(Guid DishId, double Score)> ScoreDishes(
        double[] effectiveVector,
        IEnumerable<(Guid Id, double[] Profile)> dishProfiles,
        HashSet<Guid> availableDishIds)
    {
        var results = dishProfiles
            .Where(d => availableDishIds.Contains(d.Id))
            .Select(d =>
            {
                double score = Dot(effectiveVector, d.Profile);
                return (d.Id, score);
            })
            .OrderByDescending(x => x.score)
            .ToList();

        return results;
    }

    // ── Helpers 

    private static double Dot(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += a[i] * b[i];
        return sum;
    }

    private static double StdDev(IEnumerable<double> values, double mean)
    {
        var list = values.ToList();
        if (list.Count <= 1)
            return 0;
        double variance = list.Average(v => Math.Pow(v - mean, 2));
        return Math.Sqrt(variance);
    }

    public static void ValidateChoices(string choices)
    {
        if (choices is null || choices.Length != 8)
            throw new ArgumentException("Choices must be exactly 8 characters.", nameof(choices));

        if (choices.Any(c => c != 'A' && c != 'B'))
            throw new ArgumentException("Each character must be 'A' or 'B'.", nameof(choices));
    }
}
