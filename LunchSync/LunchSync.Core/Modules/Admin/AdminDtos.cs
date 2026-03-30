namespace LunchSync.Core.Modules.Admin;

public sealed record BulkCreateRestaurantRequest(
    string Name,
    string Address,
    string? PriceTier,
    string? Status,
    string? Source
);

public sealed record BulkCreateDishRequest(
    string Name,
    string? Category
);
