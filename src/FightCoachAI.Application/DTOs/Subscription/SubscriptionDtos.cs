namespace FightCoachAI.Application.DTOs.Subscription;

public class SubscriptionResponse
{
    public Guid Id { get; set; }
    public string PlanType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MonthlyAnalysisLimit { get; set; }
    public int MaxVideoDuration { get; set; }
    public decimal Price { get; set; }
}

public class SubscriptionPlansResponse
{
    public List<PlanDto> Plans { get; set; } = new();
}

public class PlanDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AnalysisLimit { get; set; }
    public int MaxDuration { get; set; }
    public List<string> Features { get; set; } = new();
}
