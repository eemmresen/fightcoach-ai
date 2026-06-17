using FightCoachAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightCoachAI.Infrastructure.Persistence.Configurations;

public class AnalysisResultConfiguration : IEntityTypeConfiguration<AnalysisResult>
{
    public void Configure(EntityTypeBuilder<AnalysisResult> builder)
    {
        builder.ToTable("AnalysisResults");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.FightIq).IsRequired();
        builder.Property(x => x.GuardScore).IsRequired();
        builder.Property(x => x.DefenseScore).IsRequired();
        builder.Property(x => x.FootworkScore).IsRequired();
        builder.Property(x => x.AttackScore).IsRequired();
        builder.Property(x => x.ConsistencyScore).IsRequired();

        builder.HasOne(x => x.Video)
            .WithOne(x => x.AnalysisResult)
            .HasForeignKey<AnalysisResult>(x => x.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.VideoId).IsUnique();
    }
}
