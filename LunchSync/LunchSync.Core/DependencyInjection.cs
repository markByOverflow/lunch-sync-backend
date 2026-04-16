using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Modules.VotingAndScoring;
using LunchSync.Core.Modules.VotingAndScoring.Scoring;

using Microsoft.Extensions.DependencyInjection;

namespace LunchSync.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Dang ky cac service can cho auth, collection va session.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IDishService, DishService>();

            services.AddScoped<IResultsService, ResultsService>();
            // services.AddScoped<IVotingService, VotingService>();

            // Pure stateless — Singleton
            services.AddSingleton<ScoringEngine>();
            services.AddSingleton<RestaurantMatcher>();
            services.AddScoped<SessionScoringService>();

            services.AddScoped<ICollectionService, CollectionService>();

            return services;
        }
    }
}
