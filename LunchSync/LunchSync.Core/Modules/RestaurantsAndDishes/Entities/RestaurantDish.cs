namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

public class RestaurantDish
{
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public Guid DishId { get; set; } 
    public Dish Dish { get; set; } = null!;
}