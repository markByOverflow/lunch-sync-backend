using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.Interfaces;

namespace LunchSync.Core.Modules.Admin;

public sealed class AdminService : IAdminService
{
    private readonly ICurrentUserService _currentUserService;

    public AdminService(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task BulkCreateRestaurantsAsync(IReadOnlyList<BulkCreateRestaurantRequest> restaurants)
    {
        await EnsureAdminAsync();
    }

    public async Task BulkCreateDishesAsync(IReadOnlyList<BulkCreateDishRequest> dishes)
    {
        await EnsureAdminAsync();
    }

    private async Task EnsureAdminAsync()
    {
        var user = await _currentUserService.GetUserAsync();
        if (user == null || user.Role != UserRole.Admin)
        {
            throw new UnauthorizedAccessException("Admin role required.");
        }
    }
}
