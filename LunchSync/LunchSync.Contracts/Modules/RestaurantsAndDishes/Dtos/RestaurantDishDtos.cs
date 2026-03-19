namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

public sealed record RestaurantDishDto
{
    // Id nha hang
    public Guid RestaurantId { get; init; }
    // Id mon an
    public Guid DishId { get; init; }
}

public sealed record CreateRestaurantDishRequest
{
    // Id nha hang
    public Guid RestaurantId { get; init; }
    // Id mon an
    public Guid DishId { get; init; }
}
