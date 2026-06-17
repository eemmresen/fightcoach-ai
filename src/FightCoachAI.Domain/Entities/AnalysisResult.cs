namespace FightCoachAI.Domain.Entities;

public class AnalysisResult : BaseEntity
{
    public Guid VideoId { get; set; }
    public int FightIq { get; set; }
    public int GuardScore { get; set; }
    public int DefenseScore { get; set; }
    public int FootworkScore { get; set; }
    public int AttackScore { get; set; }
    public int ConsistencyScore { get; set; }
    public string? ErrorsJson { get; set; }
    public string? CombinationsJson { get; set; }
    public string? LandmarksJson { get; set; }
    public string? AiCoachFeedback { get; set; }
    public int? ProcessingTimeMs { get; set; }

    public Video? Video { get; set; }
}
