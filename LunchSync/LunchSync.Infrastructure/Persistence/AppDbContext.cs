using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Modules.Sessions.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LunchSync.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<RestaurantDish> RestaurantDishes => Set<RestaurantDish>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<RestaurantCollection> RestaurantCollections => Set<RestaurantCollection>();

    ISessionRepository IUnitOfWork.Sessions => throw new NotImplementedException();

    IDishRepository IUnitOfWork.Dishes => throw new NotImplementedException();

    IRestaurantRepository IUnitOfWork.Restaurants => throw new NotImplementedException();

    ICollectionRepository IUnitOfWork.Collections => throw new NotImplementedException();

    public Task BeginTransactionAsync(CancellationToken ct = default) => throw new NotImplementedException();
    public Task CommitAsync(CancellationToken ct = default) => throw new NotImplementedException();
    public Task RollbackAsync(CancellationToken ct = default) => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tat ca configuration khi session module duoc restore lai.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Suppress warning khi dùng owned types + lazy loading
        optionsBuilder.ConfigureWarnings(w =>
            w.Ignore(Microsoft.EntityFrameworkCore
                     .Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }
}
