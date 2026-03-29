using LunchSync.Core.Modules.RestaurantsAndDishes;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface ICollectionService
{
    // GET /collections
    Task<List<CollectionSummaryRes>> GetAllActiveCollectionsAsync(CancellationToken ct = default);

    // GET /collections/{id}
    Task<CollectionDetailRes?> GetCollectionDetailAsync(Guid id, CancellationToken ct = default);
}
