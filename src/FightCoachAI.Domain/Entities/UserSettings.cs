namespace FightCoachAI.Domain.Entities;

public class UserSettings : BaseEntity
{
    public Guid UserId { get; set; }
    public string Language { get; set; } = "tr";
    public string Units { get; set; } = "metric";
    public bool AiAnalysisAlerts { get; set; } = true;
    public bool WeeklyReports { get; set; } = true;
    public bool DarkMode { get; set; } = true;

    public User? User { get; set; }
}
