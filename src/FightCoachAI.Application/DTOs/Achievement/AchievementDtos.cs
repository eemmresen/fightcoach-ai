namespace FightCoachAI.Application.DTOs.Achievement;

public class UserAchievementResponse
{
    public string CurrentRank { get; set; } = string.Empty;
    public int TotalXp { get; set; }
    public int NextRankXp { get; set; }
    public List<EarnedBadgeDto> EarnedBadges { get; set; } = new();
    public List<InProgressBadgeDto> InProgress { get; set; } = new();
}

public class EarnedBadgeDto
{
    public string Name { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public DateTime CompletedAt { get; set; }
}

public class InProgressBadgeDto
{
    public string Name { get; set; } = string.Empty;
    public int CurrentValue { get; set; }
    public int TargetValue { get; set; }
}
