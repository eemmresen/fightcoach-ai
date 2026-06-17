namespace FightCoachAI.Domain.Entities;

public class Subscription : BaseEntity
{
    public Guid UserId { get; set; }
    public string PlanType { get; set; } = "Free";
    public string Status { get; set; } = "Active";
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public int MonthlyAnalysisLimit { get; set; } = 3;
    public int MaxVideoDuration { get; set; } = 300;
    public decimal Price { get; set; }
    public string? StripeSubscriptionId { get; set; }

    public User? User { get; set; }
}
