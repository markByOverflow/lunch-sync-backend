using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable("submissions");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(s => s.RestaurantName)
               .HasColumnName("restaurant_name")
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(s => s.Address)
               .HasColumnName("address")
               .IsRequired();

        builder.Property(s => s.GoogleMapsUrl)
               .HasColumnName("google_maps_url");

        builder.Property(s => s.PriceTier)
               .HasColumnName("price_tier")
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(s => s.Notes)
               .HasColumnName("notes");

        builder.Property(s => s.Status)
               .HasColumnName("status")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(SubmissionStatus.Pending);

        builder.Property(s => s.ReviewedBy)
               .HasColumnName("reviewed_by")
               .IsRequired(false);

        builder.Property(s => s.ReviewedAt)
               .HasColumnName("reviewed_at")
               .HasColumnType("timestamp with time zone");

        builder.Property(s => s.CreatedAt)
               .HasColumnName("created_at")
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

        // user_id tro toi nguoi submit.
        builder.HasOne(s => s.User)
               .WithMany(u => u.Submissions)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        // reviewed_by hien chi luu scalar id nguoi duyet de tranh dung chung navigation User.

        builder.HasIndex(s => s.Status)
               .HasDatabaseName("idx_submissions_status");

        builder.HasIndex(s => s.UserId)
               .HasDatabaseName("idx_submissions_user");
    }
}
