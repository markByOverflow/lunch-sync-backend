using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunchSync.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.CognitoSub)
            .HasColumnName("cognito_sub")
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(u => u.CognitoSub)
           .IsUnique()
           .HasDatabaseName("idx_users_cognito_sub");

        builder.Property(u => u.Email)
               .HasColumnName("email")
               .IsRequired()
               .HasMaxLength(255);
        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.Property(u => u.FullName)
               .HasColumnName("full_name")
               .HasMaxLength(255);

        builder.Property(u => u.Role)
               .HasColumnName("role")
               .HasConversion<string>()
               // Default role sau rebase chi con User/Admin.
               .HasDefaultValue(UserRole.User);
        builder.HasIndex(u => u.Role).HasDatabaseName("idx_users_role");

        builder.Property(u => u.IsActive)
               .HasColumnName("is_active")
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(u => u.CreatedAt)
               .HasColumnName("created_at")
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("NOW()");
    }
}
