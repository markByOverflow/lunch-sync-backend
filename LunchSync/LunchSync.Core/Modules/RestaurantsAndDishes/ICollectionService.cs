using LunchSync.Core.Modules.RestaurantsAndDishes;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface ICollectionService
{
    // GET /collections
    Task<IEnumerable<CollectionSummaryRes>> GetAllActiveCollectionsAsync();

    // GET /collections/{id}
    Task<CollectionDetailRes?> GetCollectionDetailAsync(Guid id);
}
