using LunchSync.Core.Modules.Admin;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace LunchSync.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // Giu auth/admin hien tai va them lai session service tu commit restore.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
