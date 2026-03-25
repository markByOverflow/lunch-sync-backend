
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Common.Enums;
namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public static class RestaurantMappers
{
    public static CollectionSummaryDto ToSummaryDto(Collection collection)
    {
        return new CollectionSummaryDto
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description ?? string.Empty,
            RestaurantCount = collection.RestaurantCollections?.Count(r => r.Restaurant != null && r.Restaurant.Status == RestaurantStatus.Active) ?? 0,
            CoverageRadiusMeters = collection.CoverageRadiusMeters,
            Status = collection.Status.ToString()
        };
    }
    public static CollectionDetailDto ToDetailDto(Collection collection)
    {
        return new CollectionDetailDto
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description ?? string.Empty,
            LandmarkLat = collection.LandmarkLat ?? 0,
            LandmarkLon = collection.LandmarkLon ?? 0,
            CoverageRadiusMeters = collection.CoverageRadiusMeters,
            Restaurants = collection.RestaurantCollections?
                .Where(r => r.Restaurant != null && r.Restaurant.Status == RestaurantStatus.Active)
                .Select(r => ToRestaurantBriefDto(r.Restaurant))
                .ToList() ?? new List<RestaurantBriefDto>()
        };
    }
    private static RestaurantBriefDto ToRestaurantBriefDto(Restaurant r)
    {
        return new RestaurantBriefDto
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address,
            PriceTier = r.PriceTier.ToString(),
            PriceDisplay = r.PriceTier.ToString() + "/phần",
            Rating = r.Rating ?? 0,
            ThumbnailUrl = r.ThumbnailUrl,
            FeaturedDishes = r.RestaurantDishes?
            .Where(rd => rd.Dish != null)
            .Select(rd => rd.Dish.Name).Take(3).ToList() ?? new List<string>()
        };
    }
}
