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
        // BC-1: Nước vs Khô
        new(0, "Nước vs Khô", "Nước", "Khô",
            ImpactA: MakeVector((Soupy, +0.9), (Temperature, +0.3)),
            ImpactB: MakeVector((Soupy, -0.9))),
 
        // BC-2: Nóng vs Mát
        new(1, "Nóng vs Mát", "Nóng", "Mát",
            ImpactA: MakeVector((Temperature, +0.9), (Soupy, +0.2)),
            ImpactB: MakeVector((Temperature, -0.7))),
 
        // BC-3: Nhẹ vs No nê
        new(2, "Nhẹ vs No nê", "Nhẹ", "No nê",
            ImpactA: MakeVector((Heaviness, -0.8), (TimeRequired, -0.2)),
            ImpactB: MakeVector((Heaviness, +0.9), (FlavorIntensity, +0.2))),
 
        // BC-4: Mềm vs Dai giòn
        new(3, "Mềm vs Dai giòn", "Mềm", "Dai giòn",
            ImpactA: MakeVector((TextureComplexity, -0.7), (TimeRequired, -0.1)),
            ImpactB: MakeVector((TextureComplexity, +0.8))),
 
        // BC-5: Thanh vs Đậm đà
        new(4, "Thanh vs Đậm đà", "Thanh", "Đậm đà",
            ImpactA: MakeVector((FlavorIntensity, -0.8)),
            ImpactB: MakeVector((FlavorIntensity, +0.9), (Heaviness, +0.2))),
 
        // BC-6: Không cay vs Cay
        new(5, "Không cay vs Cay", "Không cay", "Cay",
            ImpactA: MakeVector((Spicy, -0.9)),
            ImpactB: MakeVector((Spicy, +0.8), (FlavorIntensity, +0.3))),
 
        // BC-7: Nhanh vs Thong thả
        new(6, "Nhanh vs Thong thả", "Nhanh", "Thong thả",
            ImpactA: MakeVector((TimeRequired, -0.9)),
            ImpactB: MakeVector((TimeRequired, +0.7), (Novelty, +0.2))),
 
        // BC-8: Comfort vs Thử lạ
        new(7, "Comfort vs Thử lạ", "Comfort", "Thử lạ",
            ImpactA: MakeVector((Novelty, -0.8), (FlavorIntensity, -0.1)),
            ImpactB: MakeVector((Novelty, +0.9), (TextureComplexity, +0.2))),
    };

    // Helper
    private static double[] MakeVector(params (int dim, double val)[] entries)
    {
        var v = new double[DimensionCount];
        foreach (var (dim, val) in entries)
            v[dim] = val;
        return v;
    }
}
