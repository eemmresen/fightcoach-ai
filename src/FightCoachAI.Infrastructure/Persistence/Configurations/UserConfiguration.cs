using FightCoachAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCoachAI.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Discipline).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ExperienceLevel).HasMaxLength(20).IsRequired();
        builder.Property(x => x.DateOfBirth);
        builder.Property(x => x.Weight).HasColumnType("decimal(5,2)");
        builder.Property(x => x.Height).HasColumnType("decimal(5,2)");
        builder.Property(x => x.AvatarUrl).HasMaxLength(500);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.HasMany(x => x.Videos)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Subscriptions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Settings)
            .WithOne(x => x.User)
            .HasForeignKey<UserSettings>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
