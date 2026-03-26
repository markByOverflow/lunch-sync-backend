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
        builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.Pin).IsRequired().HasMaxLength(6);
        builder.HasIndex(e => e.Pin).HasDatabaseName("idx_sessions_pin");

        builder.Property(e => e.HostId).IsRequired();
        builder.HasIndex(e => e.HostId).HasDatabaseName("idx_sessions_host");

        builder.Property(e => e.CollectionId).IsRequired();

        builder.Property(e => e.PriceTier).HasConversion<string>().IsRequired();

        builder.Property(e => e.Status)
              .HasConversion<string>() //enum -> string
              .HasDefaultValue(SessionStatus.Waiting);

        // Tạo Index chỉ cho các Session chưa kết thúc (tối ưu tốc độ tìm session thực tế)
        builder.HasIndex(e => e.Status)
               .HasDatabaseName("idx_sessions_status")
               .HasFilter("status NOT IN ('done')");


        builder.Property(e => e.GroupVector).HasColumnType("jsonb");
        builder.Property(e => e.TopDishIds).HasColumnType("uuid[]");
        builder.Property(e => e.TopRestaurantIds).HasColumnType("uuid[]");
        builder.Property(e => e.BoomEliminatedIds).HasColumnType("uuid[]");
        builder.Property(e => e.FinalRestaurantId).IsRequired(false);

        builder.HasOne(s => s.Restaurant)
              .WithMany()
              .HasForeignKey(e => e.FinalRestaurantId)
              .OnDelete(DeleteBehavior.SetNull);

        builder.Property(e => e.CreatedAt).IsRequired().HasColumnType("timestamp with time zone");
        builder.Property(e => e.ExpiresAt).HasColumnType("timestamp with time zone");

        builder.HasOne(s => s.Host)
           .WithMany()
           .HasForeignKey(s => s.HostId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Collection)
           .WithMany()
           .HasForeignKey(s => s.CollectionId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
