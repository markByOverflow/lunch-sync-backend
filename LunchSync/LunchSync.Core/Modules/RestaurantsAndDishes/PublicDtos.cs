using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class RestaurantCardRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PriceTier { get; set; } = string.Empty;
    public string PriceDisplay { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string? ThumbnailUrl { get; set; }
    public List<string> FeaturedDishes { get; set; } = new();
}

public class CollectionDetailRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double LandmarkLat { get; set; }
    public double LandmarkLon { get; set; }
    public int CoverageRadiusMeters { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<RestaurantCardRes> Restaurants { get; set; } = new();
}

public class CollectionSummaryRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RestaurantCount { get; set; }
    public int CoverageRadiusMeters { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class DishListRes
{
    public List<DishSummaryRes> Dishes { get; set; } = [];
    public List<string> Categories { get; set; } = [];
    public int TotalCount { get; set; }
}

public class DishSummaryRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class DishDetailRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<ServedAtRes> ServedAt { get; set; } = [];
    public int ServedAtCount { get; set; }
}

public class ServedAtRes
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PriceTier { get; set; } = string.Empty;
    public string PriceDisplay { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Collection { get; set; }
}

public class RestaurantDetailRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? GoogleMapsUrl { get; set; }
    public string PriceTier { get; set; } = string.Empty;
    public string PriceDisplay { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string? ThumbnailUrl { get; set; }
    public List<DishSummaryRes> Dishes { get; set; } = [];
    public List<CollectionRes> Collections { get; set; } = [];
}

public class CollectionRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
