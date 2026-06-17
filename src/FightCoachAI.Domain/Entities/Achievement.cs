namespace FightCoachAI.Domain.Entities;

public class Achievement : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public int TargetValue { get; set; }
    public string AchievementType { get; set; } = string.Empty;
    public int XpReward { get; set; }

    public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
