using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;

using Microsoft.EntityFrameworkCore;

namespace LunchSync.Infrastructure.Persistence.Repositories;

public class DishRepository : IDishRepository
{
    private readonly AppDbContext _context;

    public DishRepository(AppDbContext context)
    {
        _context = context;
    }
    // Phase 3: Tìm xem món ăn này có ở những quán nào
    public async Task<Dish?> GetDishWithRestaurantsAsync(Guid id)
    {
        return await _context.Dishes
            .Include(d => d.RestaurantDishes)
                .ThenInclude(rd => rd.Restaurant)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    //Fuzzy
    public async Task<IEnumerable<Dish>> SearchDishesAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<Dish>();

        return await _context.Dishes
        .Where(d => d.Name.Contains(searchTerm) || d.Category.Contains(searchTerm))
        .OrderBy(d => d.Name.StartsWith(searchTerm) ? 0 : 1) // Ưu tiên những món bắt đầu bằng từ khóa
        .ThenBy(d => d.Name)
        .Take(5)
        .AsNoTracking()
        .ToListAsync();
    }

    // Nạp Cache cho Scoring Engine (0.01ms access)
    public async Task<IEnumerable<Dish>> GetAllDishProfilesAsync()
    {

        var data = await _context.Dishes
            .Select(d => new
            {
                d.Id,
                d.Name,
                d.Profile
            })
            .AsNoTracking()
            .ToListAsync();


        return data.Select(d => new Dish
        {
            Id = d.Id,
            Name = d.Name,
            Profile = d.Profile
        });
    }
}
