namespace FightCoachAI.Application.DTOs.Analysis;

public class AnalysisResultResponse
{
    public Guid Id { get; set; }
    public Guid VideoId { get; set; }
    public int FightIq { get; set; }
    public int GuardScore { get; set; }
    public int DefenseScore { get; set; }
    public int FootworkScore { get; set; }
    public int AttackScore { get; set; }
    public int ConsistencyScore { get; set; }
    public string? AiCoachFeedback { get; set; }
    public int? ProcessingTimeMs { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AnalysisHistoryResponse
{
    public List<AnalysisResultResponse> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class SaveAnalysisRequest
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
    public string? AiCoachFeedback { get; set; }
    public int? ProcessingTimeMs { get; set; }
}
