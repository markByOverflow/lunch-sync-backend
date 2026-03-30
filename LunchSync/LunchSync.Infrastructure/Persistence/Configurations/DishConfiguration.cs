using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class DishConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.ToTable("dishes");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(d => d.Name)
               .HasColumnName("name")
               .IsRequired()
               .HasMaxLength(100);
        builder.HasIndex(d => d.Name)
               .IsUnique();

        builder.Property(d => d.Category)
               .HasColumnName("category")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(d => d.Version)
               .HasColumnName("version")
               .IsRequired()
               .HasDefaultValue(1);

        builder.Property(d => d.UpdatedBy)
               .HasColumnName("updated_by")
               .HasMaxLength(100);

        // Profile được map sang cột "profile" kiểu JSONB
        builder.OwnsOne(d => d.Profile, profileBuilder =>
        {
            profileBuilder.ToJson("profile");
        });

        builder.ToTable(t => t.HasCheckConstraint("chk_profile_valid", "jsonb_typeof(profile) = 'object'"));

        builder.HasIndex(d => d.Profile)
               .HasMethod("gin")
               .HasDatabaseName("idx_dishes_profile");

        builder.Property(d => d.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

        builder.Property(d => d.UpdatedAt)
               .HasColumnName("updated_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");
    }
}