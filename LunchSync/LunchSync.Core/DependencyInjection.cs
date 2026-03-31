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
            // Dang ky cac service can cho auth, collection va session.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ISessionService, SessionService>();

            return services;
        }
    }
}
