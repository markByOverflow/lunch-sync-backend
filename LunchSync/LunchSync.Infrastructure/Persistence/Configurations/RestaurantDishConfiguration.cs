using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class RestaurantDishConfiguration : IEntityTypeConfiguration<RestaurantDish>
{
    public void Configure(EntityTypeBuilder<RestaurantDish> builder)
    {
        builder.ToTable("restaurant_dishes");

        builder.HasKey(rd => new { rd.RestaurantId, rd.DishId });

        builder.Property(rd => rd.RestaurantId)
               .HasColumnName("restaurant_id");

        builder.Property(rd => rd.DishId)
               .HasColumnName("dish_id");

        builder.HasIndex(rd => rd.DishId)
               .HasDatabaseName("idx_rd_dish");

        builder.HasIndex(rd => rd.RestaurantId)
               .HasDatabaseName("idx_rd_restaurant");

        builder.HasOne(rd => rd.Restaurant)
               .WithMany(r => r.RestaurantDishes)
               .HasForeignKey(rd => rd.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rd => rd.Dish)
               .WithMany(d => d.RestaurantDishes)
               .HasForeignKey(rd => rd.DishId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
