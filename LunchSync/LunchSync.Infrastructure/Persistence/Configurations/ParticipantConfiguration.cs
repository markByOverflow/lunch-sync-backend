using LunchSync.Core.Modules.Sessions.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("participants");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.SessionId)
               .HasColumnName("session_id")
               .IsRequired();

        // FK tới users(id), nullable
        builder.Property(p => p.UserId)
               .HasColumnName("user_id")
               .IsRequired(false);
        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(p => p.Nickname)
               .HasColumnName("nickname")
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(p => p.PrefVector)
               .HasColumnName("pref_vector")
               .HasColumnType("jsonb");

        builder.Property(p => p.VotedAt)
               .HasColumnName("voted_at")
               .HasColumnType("timestamp with time zone");

        builder.Property(p => p.JoinedAt)
               .HasColumnName("joined_at")
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");

        builder.HasIndex(p => new { p.SessionId, p.Nickname })
               .IsUnique();

        builder.HasIndex(p => p.SessionId)
               .HasDatabaseName("idx_participants_session");

        builder.HasOne(p => p.Session)
               .WithMany(s => s.Participants)
               .HasForeignKey(p => p.SessionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

    }
}
