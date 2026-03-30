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
            // Dang ky business service theo module de controller chi phu thuoc interface.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
