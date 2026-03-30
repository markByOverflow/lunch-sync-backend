namespace LunchSync.Core.Modules.Admin;

public interface IAdminService
{
    Task BulkCreateRestaurantsAsync(IReadOnlyList<BulkCreateRestaurantRequest> restaurants);
    Task BulkCreateDishesAsync(IReadOnlyList<BulkCreateDishRequest> dishes);
}
