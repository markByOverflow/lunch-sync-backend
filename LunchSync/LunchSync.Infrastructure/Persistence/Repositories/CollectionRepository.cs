using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

using Microsoft.EntityFrameworkCore;
namespace LunchSync.Infrastructure.Persistence.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly AppDbContext _context;

    public CollectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Collection>> GetAllActiveCollectionsAsync(CancellationToken ct = default)
    {
        return await _context.Collections
            .Include(c => c.RestaurantCollections) // Load bảng trung gian Collection-Restaurant
                .ThenInclude(rc => rc.Restaurant) // Load thông tin Restaurant để check Status
            .Where(c => c.Status == CollectionStatus.Active)
            .ToListAsync(ct);
    }

    public async Task<Collection?> GetCollectionByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Collections
            .Include(c => c.RestaurantCollections)
                .ThenInclude(rc => rc.Restaurant)
                    .ThenInclude(r => r.RestaurantDishes) // Load tiếp bảng trung gian Restaurant-Dish
                        .ThenInclude(rd => rd.Dish)      // Load thông tin Dish cuối cùng để lấy Name
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<List<Guid>> GetAvailableDishIdsInCollectionAsync(Guid collectionId, CancellationToken ct = default)
    {
        return await _context.RestaurantCollections
            .Where(rc => rc.CollectionId == collectionId)
            .SelectMany(rc => rc.Restaurant.RestaurantDishes)
            .Select(rd => rd.DishId) // Chỉ lấy ID để nhẹ truy vấn
            .Distinct()
            .ToListAsync(ct);
    }
}
