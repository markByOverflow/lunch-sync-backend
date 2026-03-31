using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Infrastructure.Auth;
using LunchSync.Infrastructure.Persistence;
using LunchSync.Infrastructure.Persistence.Caching;
using LunchSync.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace LunchSync.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            }).UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(
                configuration.GetConnectionString("Redis") ?? "localhost:6379,abortConnect=false"));

        // Bind auth options once so token services va auth provider dung chung config.
        services.Configure<GuestTokenOptions>(
            configuration.GetSection(GuestTokenOptions.SectionName));

        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddHttpClient<ICognitoAuthProvider, CognitoAuthProvider>();
        services.AddScoped<ISessionCache, SessionCache>();
        services.AddSingleton<IGuestTokenService, GuestTokenService>();

        // ── Caching ──
        //services.AddSingleton<IDishProfileCache, InMemoryDishProfileCache>();
        // ── Auth (Cognito) ──
        //services.AddScoped<ICognitoAuthProvider, CognitoAuthProvider>();


        return services;
    }
}
