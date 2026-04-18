using LunchSync.Core.Modules.VotingAndScoring.Config;

namespace LunchSync.Core.Modules.VotingAndScoring.ValueObjects;

/// <summary>
/// Immutable 8-dimension dish profile vector.
/// All values in [-1, +1].
/// Dimensions match BinaryChoiceDefinitions index order.
/// </summary>
public sealed class DishProfileVector
{
    public double Soupy { get; }
    public double Temperature { get; }
    public double Heaviness { get; }
    public double FlavorIntensity { get; }
    public double Spicy { get; }
    public double TextureComplexity { get; }
    public double TimeRequired { get; }
    public double Novelty { get; }

    public double[] Raw { get; }

    public DishProfileVector(double[] raw)
    {
        if (raw is null || raw.Length != BinaryChoiceDefinitions.DimensionCount)
            throw new ArgumentException(
                $"Profile vector must have exactly {BinaryChoiceDefinitions.DimensionCount} dimensions.", nameof(raw));

        foreach (var v in raw)
            if (v < -1.0 || v > 1.0)
                throw new ArgumentOutOfRangeException(nameof(raw), "Each dimension must be in [-1, +1].");

        Raw = (double[])raw.Clone();
        Soupy = raw[BinaryChoiceDefinitions.Soupy];
        Temperature = raw[BinaryChoiceDefinitions.Temperature];
        Heaviness = raw[BinaryChoiceDefinitions.Heaviness];
        FlavorIntensity = raw[BinaryChoiceDefinitions.FlavorIntensity];
        Spicy = raw[BinaryChoiceDefinitions.Spicy];
        TextureComplexity = raw[BinaryChoiceDefinitions.TextureComplexity];
        TimeRequired = raw[BinaryChoiceDefinitions.TimeRequired];
        Novelty = raw[BinaryChoiceDefinitions.Novelty];
    }

    /// <summary>Dot product with another raw vector (e.g. group effective vector).</summary>
    public double DotWith(double[] other)
    {
        if (other.Length != Raw.Length)
            throw new ArgumentException("Vector length mismatch.");

        double sum = 0;
        for (int i = 0; i < Raw.Length; i++)
            sum += Raw[i] * other[i];
        return sum;
    }

    public static DishProfileVector Zero =>
        new(new double[BinaryChoiceDefinitions.DimensionCount]);
}
