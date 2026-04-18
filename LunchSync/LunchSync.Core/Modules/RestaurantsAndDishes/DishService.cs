using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public sealed class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<DishListRes> GetDishesAsync(string? search, string? category, CancellationToken ct = default)
    {
        var dishes = await _dishRepository.SearchDishesAsync(search, category, ct);
        return dishes.ToDishListRes();
    }

    public async Task<DishDetailRes?> GetDishByIdAsync(Guid id, CancellationToken ct = default)
    {
        var dish = await _dishRepository.GetDishByIdAsync(id, ct);
        if (dish == null)
            return null;

        return dish.ToDishDetailRes();
    }

}
