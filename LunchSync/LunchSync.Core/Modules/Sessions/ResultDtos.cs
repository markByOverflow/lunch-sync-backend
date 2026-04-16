using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using LunchSync.Core.Common.Enums;

namespace LunchSync.Core.Modules.Sessions;


public class RestaurantResultDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    [JsonPropertyName("google_maps_url")]
    public string? GoogleMapsUrl { get; set; }
    [JsonPropertyName("price_tier")]
    public string PriceTier { get; set; } = string.Empty;
    [JsonPropertyName("price_display")]
    public string PriceDisplay { get; set; } = string.Empty;
    [JsonPropertyName("rating")]
    public double? Rating { get; set; }
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }
    [JsonPropertyName("matched_dishes")]
    public List<string> MatchedDishes { get; set; } = new();
    [JsonPropertyName("score")]
    public double Score { get; set; }
    [JsonPropertyName("rank")]
    public int Rank { get; set; }
}

public class ScoredDishDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
    [JsonPropertyName("score")]
    public double Score { get; set; }
    [JsonPropertyName("rank")]
    public int Rank { get; set; }
}
// ── GET /results ───────────────────────────────────────────────────────────

public class GetResultsDto
{
    [JsonPropertyName("top_dishes")]
    public List<ScoredDishDto> TopDishes { get; set; } = new();
    [JsonPropertyName("top_restaurants")]
    public List<RestaurantResultDto> TopRestaurants { get; set; } = new();
    // populated only after boom
    [JsonPropertyName("eliminated")]
    public List<EliminatedDto> Eliminated { get; set; } = new();
    [JsonPropertyName("remaining")]
    public List<RestaurantResultDto> Remaining { get; set; } = new();
    // populated only after pick
    [JsonPropertyName("final_restaurant")]
    public RestaurantResultDto? FinalRestaurant { get; set; }
}

// ── POST /boom ─────────────────────────────────────────────────────────────

public class BoomResultDto
{
    [JsonPropertyName("eliminated")]
    public List<EliminatedDto> Eliminated { get; set; } = new();
    [JsonPropertyName("remaining")]
    public List<RestaurantResultDto> Remaining { get; set; } = new();
}

public class EliminatedDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("rank")]
    public int Rank { get; set; }
}

// ── POST /pick ─────────────────────────────────────────────────────────────

public class PickResultDto
{
    [JsonPropertyName("final_restaurant")]
    public RestaurantResultDto FinalRestaurant { get; set; } = null!;
    [JsonPropertyName("status")]
    public SessionStatus Status { get; set; } = SessionStatus.Done;
}
