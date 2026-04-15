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
    // menu trong get restaurant
    public async Task<List<Dish>> GetDishByRestaurantsAsync(Guid restaurantId, CancellationToken ct = default)
    {
        return await _context.Dishes
            .Where(d => d.RestaurantDishes.Any(rd => rd.RestaurantId == restaurantId))
            .Select(d => new Dish { Id = d.Id, Name = d.Name, Category = d.Category })
            .AsNoTracking()
            .ToListAsync(ct);
    }
    public async Task<Dish?> GetDishByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Dishes
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }
    //Fuzzy
    public async Task<List<Dish>> SearchDishesAsync(string? searchTerm, string? category, CancellationToken ct = default)
    {
        var query = _context.Dishes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(d => EF.Functions.ILike(d.Name, $"%{searchTerm}%")
                                  || EF.Functions.ILike(d.Category, $"%{searchTerm}%"));
        }

        // 2. Lọc theo category
        if (!string.IsNullOrWhiteSpace(category))
        {
            // Dùng ILike để gõ "cơm" hay "Cơm" đều ra "Cơm"
            query = query.Where(d => EF.Functions.ILike(d.Category, $"%{category}%"));
        }

        return await query
            .OrderBy(d => d.Name)
            //.Take(5)
            .ToListAsync(ct);
    }

    // Nạp Cache cho Scoring Engine (0.01ms access)
    public async Task<List<Dish>> GetAllDishProfilesAsync(CancellationToken ct = default)
    {
        return await _context.Dishes
                    .Select(d => new Dish { Id = d.Id, Name = d.Name, Profile = d.Profile })
                    .AsNoTracking().ToListAsync(ct);
    }
}
