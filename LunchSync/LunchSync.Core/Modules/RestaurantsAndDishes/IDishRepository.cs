using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;

public interface IDishRepository
{
    Task<Dish?> GetDishWithRestaurantsAsync(Guid id);

    // Tìm kiếm món ăn theo tên hoặc danh mục
    Task<IEnumerable<Dish>> SearchDishesAsync(string searchTerm);
    // Lấy toàn bộ Profile để nạp vào IDishProfileCache (Scoring Engine)
    Task<IEnumerable<Dish>> GetAllDishProfilesAsync();
}
