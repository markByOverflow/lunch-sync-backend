using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("collections");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Name)
               .HasColumnName("name")
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .HasColumnName("description");

        builder.Property(c => c.LandmarkLat)
               .HasColumnName("landmark_lat")
               .HasColumnType("double precision");

        builder.Property(c => c.LandmarkLon)
               .HasColumnName("landmark_lng")
               .HasColumnType("double precision");

        builder.Property(c => c.CoverageRadiusMeters)
               .HasColumnName("coverage_radius_meters")
               .HasDefaultValue(250);

        builder.Property(c => c.Status)
               .HasColumnName("status")
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(CollectionStatus.Active);

        builder.Property(c => c.CreatedAt)
               .HasColumnName("created_at")
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

    }
}