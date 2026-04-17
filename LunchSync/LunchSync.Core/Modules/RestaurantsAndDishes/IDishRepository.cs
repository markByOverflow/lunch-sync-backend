using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;

public interface IDishRepository
{
    // menu trong get restaurant
    Task<List<Dish>> GetDishByRestaurantsAsync(Guid restaurantId, CancellationToken ct = default);

    Task<Dish?> GetDishByIdAsync(Guid id, CancellationToken ct = default);

    // Tìm kiếm món ăn theo tên hoặc danh mục
    Task<List<Dish>> SearchDishesAsync(string? searchTerm, string? category, CancellationToken ct = default);

    // Nạp Cache IDishProfileCache (Scoring Engine)
    Task<List<Dish>> GetAllDishProfilesAsync(CancellationToken ct = default);

    // Spec Returns distinct dish IDs available through a collection's restaurants.
    Task<List<Guid>> GetAvailableDishIdsByCollectionAsync(Guid collectionId, CancellationToken ct = default);
}
