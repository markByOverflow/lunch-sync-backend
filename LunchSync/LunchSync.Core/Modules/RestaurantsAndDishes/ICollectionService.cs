using LunchSync.Core.Modules.RestaurantsAndDishes;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface ICollectionService
{
    // GET /collections
    Task<IEnumerable<CollectionSummaryDto>> GetAllActiveCollectionsAsync();

    // GET /collections/{id}
    Task<CollectionDetailDto?> GetCollectionDetailAsync(Guid id);
}
