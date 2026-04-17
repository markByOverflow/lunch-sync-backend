using System.Text.Json;

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

        builder.Property(e => e.Nickname)
        .HasColumnName("nickname")
        .IsRequired().HasMaxLength(20);

        builder.Property(e => e.SessionId)          // ← thêm
            .HasColumnName("session_id")
            .IsRequired();

        // Quan hệ 1-N: Một Session có nhiều Participants
        builder.HasOne(p => p.Session) //1 session
                      .WithMany(s => s.Participants) //Session nhieu Participants
                      .HasForeignKey(p => p.SessionId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); // Xóa Session thì xóa sạch Participant

        builder.HasIndex(e => new { e.SessionId, e.Nickname }).IsUnique();
        builder.HasIndex(e => e.SessionId).HasDatabaseName("idx_participants_session");

        builder.Property(e => e.PrefVector)
        .HasColumnName("pref_vector")
        .HasColumnType("jsonb")
        .HasConversion(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default), // Chuyển List thành chuỗi JSON
            v => JsonSerializer.Deserialize<List<float>>(v, JsonSerializerOptions.Default) ?? new List<float>() // Đọc ngược lại
        );

        builder.Property(e => e.JoinedAt)
        .HasColumnName("joined_at")
        .IsRequired()
        .HasColumnType("timestamp with time zone")
        .HasDefaultValueSql("NOW()");

        builder.Property(e => e.VotedAt)
        .HasColumnName("voted_at")
        .HasColumnType("timestamp with time zone");

    }
}
