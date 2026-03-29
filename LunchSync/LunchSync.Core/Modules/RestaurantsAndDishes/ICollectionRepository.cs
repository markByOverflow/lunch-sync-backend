using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface ICollectionRepository
{
    Task<List<Collection>> GetAllActiveCollectionsAsync(CancellationToken ct = default);
    Task<Collection?> GetCollectionByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Guid>> GetAvailableDishIdsInCollectionAsync(Guid collectionId, CancellationToken ct = default);
}
