using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSync.Core.Modules.VotingAndScoring.Config;

/// <summary>
/// Định nghĩa một lựa chọn nhị phân (A/B), impact vectors lên 8 chiều.
/// Dimensions: [soupy, temperature, heaviness, flavor_intensity, spicy, texture_complexity, time_required, novelty]
/// </summary>
public sealed record BinaryChoice(
    int Index,
    string Label,
    string OptionA,
    string OptionB,
    double[] ImpactA,   // length = 8
    double[] ImpactB    // length = 8
);

public static class BinaryChoiceDefinitions
{
    // Dimension index constants
    public const int Soupy = 0;
    public const int Temperature = 1;
    public const int Heaviness = 2;
    public const int FlavorIntensity = 3;
    public const int Spicy = 4;
    public const int TextureComplexity = 5;
    public const int TimeRequired = 6;
    public const int Novelty = 7;

    public const int DimensionCount = 8;

    /// <summary>
    /// 8 binary choices, indexed 0–7 (BC-1 … BC-8).
    /// ImpactA / ImpactB are sparse — unspecified dimensions default to 0.
    /// </summary>
    public static readonly IReadOnlyList<BinaryChoice> All = new List<BinaryChoice>
    {

    };

    // Helper
    private static int MakeVector(params (int dim, double val)[] entries)
    {
        return 0; // Placeholder, actual implementation would create a vector of length 8 with specified values
    }
}
