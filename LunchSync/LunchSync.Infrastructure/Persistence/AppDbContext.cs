using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Modules.Sessions.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LunchSync.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<RestaurantCollection> RestaurantCollections => Set<RestaurantCollection>();
    public DbSet<RestaurantDish> RestaurantDishes => Set<RestaurantDish>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tat ca configuration khi session module duoc restore lai.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
