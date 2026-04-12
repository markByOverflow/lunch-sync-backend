using LunchSync.Core.Common.Abstractions;
using LunchSync.Core.Common.ValueObjects;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

public class Dish : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public DishProfile Profile { get; set; } = new();
    public int Version { get; set; } = 1;
    public string? UpdatedBy { get; set; }

    // Navigation Property
    public ICollection<RestaurantDish> RestaurantDishes { get; set; } = new List<RestaurantDish>();
}
