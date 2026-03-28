using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.Sessions;

using Microsoft.Extensions.DependencyInjection;

namespace LunchSync.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // Auth
            //services.AddScoped<IAuthService, AuthService>();

            // Sessions
            services.AddScoped<ISessionService, SessionService>();
            //services.AddScoped<IResultsService, ResultsService>();

            // Voting & Scoring
            //services.AddScoped<IVotingService, VotingService>();
            //services.AddScoped<SessionScoringService>();    // orchestrator
            //services.AddSingleton<ScoringEngine>();         // stateless math — singleton OK

            // Restaurants & Dishes
            services.AddScoped<ICollectionService, CollectionService>();
            //services.AddScoped<ISubmissionService, SubmissionService>();

            // Admin
            //services.AddScoped<IAdminService, AdminService>();
            return services;
        }
    }
}
