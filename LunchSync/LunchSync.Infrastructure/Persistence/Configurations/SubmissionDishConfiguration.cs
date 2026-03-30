using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class SubmissionDishConfiguration : IEntityTypeConfiguration<SubmissionDish>
{
    public void Configure(EntityTypeBuilder<SubmissionDish> builder)
    {
        builder.ToTable("submission_dishes");

        // Composite key de moi mon trong mot submission chi xuat hien mot lan.
        builder.HasKey(sd => new { sd.SubmissionId, sd.DishId });

        builder.Property(sd => sd.SubmissionId)
               .HasColumnName("submission_id");

        builder.Property(sd => sd.DishId)
               .HasColumnName("dish_id");

        builder.HasOne(sd => sd.Submission)
               .WithMany(s => s.SubmissionDishes)
               .HasForeignKey(sd => sd.SubmissionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sd => sd.Dish)
               .WithMany(d => d.SubmissionDishes)
               .HasForeignKey(sd => sd.DishId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
