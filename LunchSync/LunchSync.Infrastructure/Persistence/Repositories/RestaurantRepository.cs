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
    public async Task<Restaurant?> GetRestaurantWithDishesAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Restaurants
            .Include(r => r.RestaurantDishes)      // Vào bảng trung gian
                .ThenInclude(rd => rd.Dish)        // Vào bảng Dish để lấy Name
            .Include(r => r.RestaurantCollections)
                .ThenInclude(rc => rc.Collection)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    // 2. Lấy danh sách nhà hàng theo danh sách ID
    public async Task<List<Restaurant>> GetRestaurantsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        if (ids == null || !ids.Any())
            return new List<Restaurant>();

        return await _context.Restaurants
            .Include(r => r.RestaurantDishes)
                .ThenInclude(rd => rd.Dish)
            .Include(r => r.RestaurantCollections)
                .ThenInclude(rc => rc.Collection)
            .Where(r => ids.Contains(r.Id))
            .AsNoTracking()
            .ToListAsync(ct);
    }
    //Danh sách nhà hàng theo top dishes
    public async Task<List<Restaurant>> GetRestaurantsByTopDishesAsync(Guid collectionId, IEnumerable<Guid> topDishIds, CancellationToken ct = default)
    {
        return await _context.RestaurantCollections
            .Where(rc => rc.CollectionId == collectionId)
            .Include(rc => rc.Restaurant)
                .ThenInclude(r => r.RestaurantDishes)
                    .ThenInclude(rd => rd.Dish)
            .Select(rc => rc.Restaurant) // Select phải nằm ở cuối cùng
            .Where(r => r.RestaurantDishes.Any(rd => topDishIds.Contains(rd.DishId)))
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
