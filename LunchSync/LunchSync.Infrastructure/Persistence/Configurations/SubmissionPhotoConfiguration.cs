using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class SubmissionPhotoConfiguration : IEntityTypeConfiguration<SubmissionPhoto>
{
    public void Configure(EntityTypeBuilder<SubmissionPhoto> builder)
    {
        builder.ToTable("submission_photos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.SubmissionId)
               .HasColumnName("submission_id")
               .IsRequired();

        builder.Property(p => p.PhotoUrl)
               .HasColumnName("photo_url")
               .IsRequired();

        builder.Property(p => p.DisplayOrder)
               .HasColumnName("display_order")
               .IsRequired()
               .HasDefaultValue(0);

        builder.Property(p => p.CreatedAt)
               .HasColumnName("created_at")
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

        builder.HasOne(p => p.Submission)
               .WithMany(s => s.Photos)
               .HasForeignKey(p => p.SubmissionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.SubmissionId)
               .HasDatabaseName("idx_submission_photos_sub");
    }
}
