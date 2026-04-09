using System.Text.Json.Serialization;

using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class RestaurantCardRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("price_tier")]
    public string PriceTier { get; set; } = string.Empty;

    [JsonPropertyName("price_display")]
    public string PriceDisplay { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    [JsonPropertyName("featured_dishes")]
    public List<string> FeaturedDishes { get; set; } = new();
}

public class CollectionDetailRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("landmark_lat")]
    public double LandmarkLat { get; set; }

    [JsonPropertyName("landmark_lon")]
    public double LandmarkLon { get; set; }

    [JsonPropertyName("coverage_radius_meters")]
    public int CoverageRadiusMeters { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("restaurants")]
    public List<RestaurantCardRes> Restaurants { get; set; } = new();
}

public class CollectionSummaryRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("restaurant_count")]
    public int RestaurantCount { get; set; }

    [JsonPropertyName("coverage_radius_meters")]
    public int CoverageRadiusMeters { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public class DishListRes
{
    [JsonPropertyName("dishes")]
    public List<DishSummaryRes> Dishes { get; set; } = [];

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = [];

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
}

public class DishSummaryRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}

public class DishDetailRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("served_at")]
    public List<ServedAtRes> ServedAt { get; set; } = [];

    [JsonPropertyName("served_at_count")]
    public int ServedAtCount { get; set; }
}

public class ServedAtRes
{
    [JsonPropertyName("restaurant_id")]
    public Guid RestaurantId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("price_tier")]
    public string PriceTier { get; set; } = string.Empty;

    [JsonPropertyName("price_display")]
    public string PriceDisplay { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    [JsonPropertyName("collection")]
    public string? Collection { get; set; }
}

public class RestaurantDetailRes
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
    public double Rating { get; set; }

    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    [JsonPropertyName("dishes")]
    public List<DishSummaryRes> Dishes { get; set; } = [];

    [JsonPropertyName("collections")]
    public List<CollectionRes> Collections { get; set; } = [];
}

public class CollectionRes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
