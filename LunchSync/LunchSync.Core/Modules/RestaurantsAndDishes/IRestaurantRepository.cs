using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface IRestaurantRepository
{
    // Lấy chi tiết 1 nhà hàng kèm theo danh sách món ăn (để map FeaturedDishes)
    Task<Restaurant?> GetRestaurantWithDishesAsync(Guid id, CancellationToken ct = default);

    // Lấy danh sách nhà hàng theo danh sách ID (dùng cho logic xử lý sau voting)
    Task<List<Restaurant>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    /// Lấy danh sách nhà hàng phục vụ ít nhất 1 món trong top dish
    Task<List<Restaurant>> GetRestaurantsByTopDishesAsync(Guid collectionId, IEnumerable<Guid> topDishIds, CancellationToken ct = default);
}
