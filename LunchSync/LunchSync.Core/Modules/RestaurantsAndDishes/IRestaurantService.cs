using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface IRestaurantService
{
    Task<RestaurantDetailRes?> GetRestaurantWithDishesAsync(Guid id, CancellationToken ct = default);
}
