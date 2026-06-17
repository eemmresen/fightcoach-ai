namespace FightCoachAI.Domain.Entities;

public class Video : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string? ProcessedUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public int? Duration { get; set; }
    public string? Resolution { get; set; }
    public long? FileSize { get; set; }
    public string Status { get; set; } = "Uploaded";
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public User? User { get; set; }
    public AnalysisResult? AnalysisResult { get; set; }
}
