using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Sessions.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");
        builder.HasKey(e => e.Id);
        builder.Property(s => s.Id)
              .HasColumnName("id")
              .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Pin)
               .HasColumnName("pin")
               .IsRequired()
               .HasMaxLength(6);
        builder.HasIndex(s => s.Pin)
               .IsUnique()
               .HasDatabaseName("idx_sessions_pin");

        builder.Property(s => s.HostId)
               .HasColumnName("host_id")
               .IsRequired();
        builder.HasIndex(s => s.HostId)
               .HasDatabaseName("idx_sessions_host");

        builder.Property(s => s.CollectionId)
               .HasColumnName("collection_id")
               .IsRequired();

        builder.Property(s => s.PriceTier)
               .HasColumnName("price_tier")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(s => s.Status)
               .HasColumnName("status")
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasDefaultValue(SessionStatus.Waiting);

        builder.HasIndex(s => s.Status)
               .HasDatabaseName("idx_sessions_status")
               .HasFilter("status NOT IN ('done')");

        builder.Property(s => s.GroupVector)
               .HasColumnName("group_vector")
               .HasColumnType("jsonb");

        builder.Property(s => s.TopDishIds)
               .HasColumnName("top_dish_ids")
               .HasColumnType("uuid[]");

        builder.Property(s => s.TopRestaurantIds)
               .HasColumnName("top_restaurant_ids")
               .HasColumnType("uuid[]");

        builder.Property(s => s.BoomEliminatedIds)
               .HasColumnName("boom_eliminated_ids")
               .HasColumnType("uuid[]");

        builder.Property(s => s.FinalRestaurantId)
               .HasColumnName("final_restaurant_id")
               .IsRequired(false);

        builder.Property(s => s.VotingStartedAt)
               .HasColumnName("voting_started_at")
               .HasColumnType("timestamp with time zone");

        builder.Property(s => s.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

        builder.Property(s => s.ExpiresAt)
               .HasColumnName("expires_at")
               .HasColumnType("timestamp with time zone");

        builder.HasOne(s => s.Host)
               .WithMany()
               .HasForeignKey(s => s.HostId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Collection)
               .WithMany()
               .HasForeignKey(s => s.CollectionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Restaurant)
               .WithMany()
               .HasForeignKey(s => s.FinalRestaurantId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
