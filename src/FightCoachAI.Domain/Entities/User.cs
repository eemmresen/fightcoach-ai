namespace FightCoachAI.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Discipline { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Video> Videos { get; set; } = new List<Video>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    public UserSettings? Settings { get; set; }
}
