using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class SubmissionDishConfiguration : IEntityTypeConfiguration<SubmissionDish>
{
    public void Configure(EntityTypeBuilder<SubmissionDish> builder)
    {
        builder.ToTable("submission_dishes");

        // Composite Key: PRIMARY KEY (submission_id, dish_id)
        builder.HasKey(sd => new { sd.SubmissionId, sd.DishId });

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
