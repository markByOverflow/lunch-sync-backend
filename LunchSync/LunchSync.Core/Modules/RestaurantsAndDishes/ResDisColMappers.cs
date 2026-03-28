
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Common.Enums;
namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public static class ResDisColMappers
{
    //collection
    public static CollectionSummaryRes ToCollectionSummaryRes(this Collection collection)
    {
        return new CollectionSummaryRes
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description ?? string.Empty,
            RestaurantCount = collection.RestaurantCollections?.Count(r => r.Restaurant != null && r.Restaurant.Status == RestaurantStatus.Active) ?? 0,
            CoverageRadiusMeters = collection.CoverageRadiusMeters,
            Status = collection.Status.ToString()
        };
    }
    public static CollectionDetailRes ToCollectionDetailRes(this Collection collection)
    {
        return new CollectionDetailRes
        {
            Id = collection.Id,
            Name = collection.Name,
            Description = collection.Description ?? string.Empty,
            LandmarkLat = collection.LandmarkLat ?? 0,
            LandmarkLon = collection.LandmarkLon ?? 0,
            CoverageRadiusMeters = collection.CoverageRadiusMeters,
            Restaurants = collection.RestaurantCollections?
                .Where(r => r.Restaurant != null && r.Restaurant.Status == RestaurantStatus.Active)
                .Select(r => ToRestaurantCardRes(r.Restaurant))
                .ToList() ?? new List<RestaurantCardRes>()
        };
    }
    //Dish
    public static DishSummaryRes ToDishSummaryRes(this Dish dish)
    {
        return new DishSummaryRes
        {
            Id = dish.Id,
            Name = dish.Name,
            Category = dish.Category
        };
    }

    public static DishDetailRes ToDishDetailRes(this Dish dish)
    {
        var servedAt = dish.RestaurantDishes
            .Select(rd => rd.Restaurant.ToServedAtRes())
            .ToList();

        return new DishDetailRes
        {
            Id = dish.Id,
            Name = dish.Name,
            Category = dish.Category,
            ServedAt = servedAt,
            ServedAtCount = servedAt.Count
        };
    }

    public static ServedAtRes ToServedAtRes(this Restaurant restaurant)
    {
        // Pick the first collection name as the display collection
        var collectionName = restaurant.RestaurantCollections
            .Select(rc => rc.Collection.Name)
            .FirstOrDefault();

        return new ServedAtRes
        {
            RestaurantId = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            PriceTier = restaurant.PriceTier.ToString(),
            PriceDisplay = restaurant.PriceTier.ToString() + "/phần",
            Rating = restaurant.Rating ?? 0,
            ThumbnailUrl = restaurant.ThumbnailUrl,
            Collection = collectionName
        };
    }

    public static DishListRes ToDishListRes(this IEnumerable<Dish> dishes)
    {
        var categories = dishes
            .Select(d => d.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        return new DishListRes
        {
            Dishes = dishes.Select(d => d.ToDishSummaryRes()).ToList(),
            Categories = categories,
            TotalCount = dishes.Count()
        };
    }
    //Restaurant
    private static RestaurantCardRes ToRestaurantCardRes(this Restaurant r)
    {
        return new RestaurantCardRes
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
    public static RestaurantDetailRes ToRestaurantDetailRes(this Restaurant restaurant)
    {
        return new RestaurantDetailRes
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            GoogleMapsUrl = restaurant.GoogleMapsUrl,
            PriceTier = restaurant.PriceTier.ToString(),
            PriceDisplay = restaurant.PriceTier.ToString() + "/phần",
            Rating = restaurant.Rating ?? 0,
            ThumbnailUrl = restaurant.ThumbnailUrl,

            Dishes = restaurant.RestaurantDishes
                .Select(rd => rd.Dish.ToDishSummaryRes())
                .ToList(),

            Collections = restaurant.RestaurantCollections
                .Select(rc => new CollectionRes
                {
                    Id = rc.Collection.Id,
                    Name = rc.Collection.Name
                })
                .ToList()
        };
    }

}
