using FightCoachAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCoachAI.Infrastructure.Persistence.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.ToTable("UserSettings");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Language).HasMaxLength(10).HasDefaultValue("tr");
        builder.Property(x => x.Units).HasMaxLength(10).HasDefaultValue("metric");

        builder.HasOne(x => x.User)
            .WithOne(x => x.Settings)
            .HasForeignKey<UserSettings>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}
