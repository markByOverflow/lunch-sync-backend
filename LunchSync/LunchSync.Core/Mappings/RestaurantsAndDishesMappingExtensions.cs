using LunchSync.Contracts.Common.Enums;
using LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;
using LunchSync.Core.Common.ValueObjects;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Mappings;

public static class RestaurantsAndDishesMappingExtensions
{
    public static CollectionDto ToDto(this Collection entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new CollectionDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            LandmarkLat = entity.LandmarkLat,
            LandmarkLon = entity.LandmarkLon,
            CoverageRadiusMeters = entity.CoverageRadiusMeters,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Status = Enum.Parse<CollectionStatus>(entity.Status.ToString()),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Collection ToEntity(this CreateCollectionRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new Collection
        {
            Name = request.Name,
            Description = request.Description,
            LandmarkLat = request.LandmarkLat,
            LandmarkLon = request.LandmarkLon,
            CoverageRadiusMeters = request.CoverageRadiusMeters
        };
    }

    public static void ApplyTo(this UpdateCollectionRequest request, Collection entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.LandmarkLat = request.LandmarkLat;
        entity.LandmarkLon = request.LandmarkLon;
        entity.CoverageRadiusMeters = request.CoverageRadiusMeters;
    }

    public static DishDto ToDto(this Dish entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Null safety: neu Profile null thi dung gia tri mac dinh
        var profile = entity.Profile ?? new DishProfile();

        // Map tung truong tu entity sang DTO (flatten DishProfile)
        return new DishDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            Soupy = profile.Soupy,
            Temperature = profile.Temperature,
            Heaviness = profile.Heaviness,
            FlavorIntensity = profile.FlavorIntensity,
            Spicy = profile.Spicy,
            TextureComplexity = profile.TextureComplexity,
            TimeRequired = profile.TimeRequired,
            Novelty = profile.Novelty,
            Healthy = profile.Healthy,
            Communal = profile.Communal,
            Version = entity.Version,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Dish ToEntity(this CreateDishRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (tao DishProfile tu cac truong phang)
        return new Dish
        {
            Name = request.Name,
            Category = request.Category,
            Profile = new DishProfile
            {
                Soupy = request.Soupy,
                Temperature = request.Temperature,
                Heaviness = request.Heaviness,
                FlavorIntensity = request.FlavorIntensity,
                Spicy = request.Spicy,
                TextureComplexity = request.TextureComplexity,
                TimeRequired = request.TimeRequired,
                Novelty = request.Novelty,
                Healthy = request.Healthy,
                Communal = request.Communal
            },
            Version = request.Version
        };
    }

    public static void ApplyTo(this UpdateDishRequest request, Dish entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity (cap nhat DishProfile)
        entity.Name = request.Name;
        entity.Category = request.Category;
        entity.Profile = new DishProfile
        {
            Soupy = request.Soupy,
            Temperature = request.Temperature,
            Heaviness = request.Heaviness,
            FlavorIntensity = request.FlavorIntensity,
            Spicy = request.Spicy,
            TextureComplexity = request.TextureComplexity,
            TimeRequired = request.TimeRequired,
            Novelty = request.Novelty,
            Healthy = request.Healthy,
            Communal = request.Communal
        };
        entity.Version = request.Version;
    }

    public static RestaurantDto ToDto(this Restaurant entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new RestaurantDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            GoogleMapsUrl = entity.GoogleMapsUrl,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = Enum.Parse<PriceTier>(entity.PriceTier.ToString()),
            Rating = entity.Rating,
            ThumbnailUrl = entity.ThumbnailUrl,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Status = Enum.Parse<RestaurantStatus>(entity.Status.ToString()),
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Source = Enum.Parse<RestaurantSource>(entity.Source.ToString()),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Restaurant ToEntity(this CreateRestaurantRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new Restaurant
        {
            Name = request.Name,
            Address = request.Address,
            GoogleMapsUrl = request.GoogleMapsUrl,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.ToString()),
            Rating = request.Rating,
            ThumbnailUrl = request.ThumbnailUrl
        };
    }

    public static void ApplyTo(this UpdateRestaurantRequest request, Restaurant entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.GoogleMapsUrl = request.GoogleMapsUrl;
        // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
        entity.PriceTier = Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.ToString());
        entity.Rating = request.Rating;
        entity.ThumbnailUrl = request.ThumbnailUrl;
    }

    public static RestaurantCollectionDto ToDto(this RestaurantCollection entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new RestaurantCollectionDto
        {
            RestaurantId = entity.RestaurantId,
            CollectionId = entity.CollectionId
        };
    }

    public static RestaurantCollection ToEntity(this CreateRestaurantCollectionRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity
        return new RestaurantCollection
        {
            RestaurantId = request.RestaurantId,
            CollectionId = request.CollectionId
        };
    }

    public static RestaurantDishDto ToDto(this RestaurantDish entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new RestaurantDishDto
        {
            RestaurantId = entity.RestaurantId,
            DishId = entity.DishId
        };
    }

    public static RestaurantDish ToEntity(this CreateRestaurantDishRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity
        return new RestaurantDish
        {
            RestaurantId = request.RestaurantId,
            DishId = request.DishId
        };
    }

    public static SubmissionDto ToDto(this Submission entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new SubmissionDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            RestaurantName = entity.RestaurantName,
            Address = entity.Address,
            GoogleMapsUrl = entity.GoogleMapsUrl,
            // Chuyen doi enum nullable bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = entity.PriceTier is null
                ? null
                : Enum.Parse<PriceTier>(entity.PriceTier.Value.ToString()),
            Note = entity.Note,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Status = Enum.Parse<SubmissionStatus>(entity.Status.ToString()),
            ReviewedBy = entity.ReviewedBy,
            ReviewedAt = entity.ReviewedAt,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Submission ToEntity(this CreateSubmissionRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new Submission
        {
            UserId = request.UserId,
            RestaurantName = request.RestaurantName,
            Address = request.Address,
            GoogleMapsUrl = request.GoogleMapsUrl,
            // Chuyen doi enum nullable bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = request.PriceTier is null
                ? null
                : Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.Value.ToString()),
            Note = request.Note
        };
    }

    public static void ApplyTo(this UpdateSubmissionRequest request, Submission entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.UserId = request.UserId;
        entity.RestaurantName = request.RestaurantName;
        entity.Address = request.Address;
        entity.GoogleMapsUrl = request.GoogleMapsUrl;
        // Chuyen doi enum nullable bang Enum.Parse de tranh ep kieu truc tiep
        entity.PriceTier = request.PriceTier is null
            ? null
            : Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.Value.ToString());
        entity.Note = request.Note;
    }

    public static SubmissionDishDto ToDto(this SubmissionDish entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new SubmissionDishDto
        {
            SubmissionId = entity.SubmissionId,
            DishId = entity.DishId
        };
    }

    public static SubmissionDish ToEntity(this CreateSubmissionDishRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity
        return new SubmissionDish
        {
            SubmissionId = request.SubmissionId,
            DishId = request.DishId
        };
    }

    public static SubmissionPhotoDto ToDto(this SubmissionPhoto entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new SubmissionPhotoDto
        {
            Id = entity.Id,
            SubmissionId = entity.SubmissionId,
            PhotoUrl = entity.PhotoUrl,
            DisplayOrder = entity.DisplayOrder,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static SubmissionPhoto ToEntity(this CreateSubmissionPhotoRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity
        return new SubmissionPhoto
        {
            SubmissionId = request.SubmissionId,
            PhotoUrl = request.PhotoUrl,
            DisplayOrder = request.DisplayOrder
        };
    }

    public static void ApplyTo(this UpdateSubmissionPhotoRequest request, SubmissionPhoto entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai
        entity.SubmissionId = request.SubmissionId;
        entity.PhotoUrl = request.PhotoUrl;
        entity.DisplayOrder = request.DisplayOrder;
    }
}
