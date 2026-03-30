using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("restaurants");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(r => r.Name)
               .HasColumnName("name")
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(r => r.Address)
               .HasColumnName("address")
               .IsRequired();

        builder.Property(r => r.GoogleMapsUrl)
               .HasColumnName("google_maps_url");

        builder.Property(r => r.PriceTier)
               .HasColumnName("price_tier")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(r => r.Rating)
               .HasColumnName("rating")
               .HasColumnType("double precision");

        builder.Property(r => r.ThumbnailUrl)
               .HasColumnName("thumbnail_url");

        builder.Property(r => r.Status)
               .HasColumnName("status")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(RestaurantStatus.Active);

        builder.Property(r => r.Source)
               .HasColumnName("source")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(RestaurantSource.Admin);

        builder.Property(r => r.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");
    }
}