using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

using Microsoft.EntityFrameworkCore;

namespace LunchSync.Infrastructure.Persistence.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context; // Thay bằng tên DbContext của bạn

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    // 1. Lấy chi tiết 1 nhà hàng kèm theo danh sách món ăn
    public async Task<Restaurant?> GetRestaurantWithDishesAsync(Guid id)
    {
        return await _context.Restaurants
            .Include(r => r.RestaurantDishes)      // Vào bảng trung gian
                .ThenInclude(rd => rd.Dish)        // Vào bảng Dish để lấy Name
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    // 2. Lấy danh sách nhà hàng theo danh sách ID
    public async Task<IEnumerable<Restaurant>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            return Enumerable.Empty<Restaurant>();

        return await _context.Restaurants
            .Include(r => r.RestaurantDishes)
                .ThenInclude(rd => rd.Dish)
            .Where(r => ids.Contains(r.Id))
            .AsNoTracking()
            .ToListAsync();
    }
    //Danh sách nhà hàng theo top dishes
    public async Task<IEnumerable<Restaurant>> GetRestaurantsByTopDishesAsync(Guid collectionId, IEnumerable<Guid> topDishIds)
    {
        return await _context.RestaurantCollections
            .Where(rc => rc.CollectionId == collectionId)
            .Select(rc => rc.Restaurant)
            .Include(r => r.RestaurantDishes)
                .ThenInclude(rd => rd.Dish)
            .Where(r => r.RestaurantDishes.Any(rd => topDishIds.Contains(rd.DishId)))
            .AsNoTracking()
            .ToListAsync();
    }
}
