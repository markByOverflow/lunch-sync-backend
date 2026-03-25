using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetAllActiveCollectionsAsync();
    Task<Collection?> GetCollectionByIdAsync(Guid id);
}
