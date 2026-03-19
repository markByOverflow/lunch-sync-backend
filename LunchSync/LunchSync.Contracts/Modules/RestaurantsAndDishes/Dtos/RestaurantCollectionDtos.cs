namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

public sealed record RestaurantCollectionDto
{
    // Id nha hang
    public Guid RestaurantId { get; init; }
    // Id bo suu tap
    public Guid CollectionId { get; init; }
}

public sealed record CreateRestaurantCollectionRequest
{
    // Id nha hang
    public Guid RestaurantId { get; init; }
    // Id bo suu tap
    public Guid CollectionId { get; init; }
}
