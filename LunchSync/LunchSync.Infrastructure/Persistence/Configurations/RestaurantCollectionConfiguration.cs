using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class RestaurantCollectionConfiguration : IEntityTypeConfiguration<RestaurantCollection>
{
    public void Configure(EntityTypeBuilder<RestaurantCollection> builder)
    {
        builder.ToTable("restaurant_collections");

        builder.HasKey(rc => new { rc.RestaurantId, rc.CollectionId });

        builder.Property(rc => rc.RestaurantId)
               .HasColumnName("restaurant_id");

        builder.Property(rc => rc.CollectionId)
               .HasColumnName("collection_id");

        builder.HasIndex(rc => rc.CollectionId)
               .HasDatabaseName("idx_rc_collection");

        builder.HasOne(rc => rc.Restaurant)
               .WithMany(r => r.RestaurantCollections)
               .HasForeignKey(rc => rc.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rc => rc.Collection)
               .WithMany(c => c.RestaurantCollections)
               .HasForeignKey(rc => rc.CollectionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
