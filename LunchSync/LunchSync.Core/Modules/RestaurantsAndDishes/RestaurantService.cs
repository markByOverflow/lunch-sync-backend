using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<RestaurantDetailRes?> GetRestaurantWithDishesAsync(Guid id, CancellationToken ct = default)
    {
        var restaurant = await _restaurantRepository.GetRestaurantWithDishesAsync(id, ct);
        return restaurant?.ToRestaurantDetailRes();
    }

}
