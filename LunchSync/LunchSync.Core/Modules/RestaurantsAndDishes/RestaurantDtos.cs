using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class RestaurantBriefDto
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

public class CollectionDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double LandmarkLat { get; set; }
    public double LandmarkLon { get; set; }
    public int CoverageRadiusMeters { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<RestaurantBriefDto> Restaurants { get; set; } = new();
}

public class CollectionSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RestaurantCount { get; set; }
    public int CoverageRadiusMeters { get; set; }
    public string Status { get; set; } = string.Empty;
}
