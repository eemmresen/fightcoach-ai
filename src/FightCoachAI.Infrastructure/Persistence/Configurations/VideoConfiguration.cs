using FightCoachAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCoachAI.Infrastructure.Persistence.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.OriginalUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.ProcessedUrl).HasMaxLength(500);
        builder.Property(x => x.ThumbnailUrl).HasMaxLength(500);
        builder.Property(x => x.Resolution).HasMaxLength(20);
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Videos)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AnalysisResult)
            .WithOne(x => x.Video)
            .HasForeignKey<AnalysisResult>(x => x.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.UserId, x.Status });
    }
}
