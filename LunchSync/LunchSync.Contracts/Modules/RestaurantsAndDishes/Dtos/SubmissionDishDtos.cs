namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

public sealed record SubmissionDishDto
{
    // Id de xuat
    public Guid SubmissionId { get; init; }
    // Id mon an
    public Guid DishId { get; init; }
}

public sealed record CreateSubmissionDishRequest
{
    // Id de xuat
    public Guid SubmissionId { get; init; }
    // Id mon an
    public Guid DishId { get; init; }
}
