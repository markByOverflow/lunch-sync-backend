namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public interface IDishService
{
    Task<DishListRes> GetDishesAsync(string? search, string? category, CancellationToken ct = default);
    Task<DishDetailRes?> GetDishByIdAsync(Guid id, CancellationToken ct = default);
}
