namespace FightCoachAI.Application.DTOs.Video;

public class VideoUploadResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Title { get; set; }
    public DateTime UploadedAt { get; set; }
}

public class VideoDetailResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public int? Duration { get; set; }
    public string? Resolution { get; set; }
    public long? FileSize { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public AnalysisBriefDto? Analysis { get; set; }
}

public class AnalysisBriefDto
{
    public int FightIq { get; set; }
    public int GuardScore { get; set; }
    public int DefenseScore { get; set; }
    public int FootworkScore { get; set; }
    public int AttackScore { get; set; }
}

public class VideoListResponse
{
    public List<VideoDetailResponse> Videos { get; set; } = new();
    public int TotalCount { get; set; }
    public int MonthlyUsed { get; set; }
    public int MonthlyLimit { get; set; }
}
