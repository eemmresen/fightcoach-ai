namespace FightCoachAI.Domain.Entities;

public class UserAchievement : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid AchievementId { get; set; }
    public int CurrentValue { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }

    public User? User { get; set; }
    public Achievement? Achievement { get; set; }
}
