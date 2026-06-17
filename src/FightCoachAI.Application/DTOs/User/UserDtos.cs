namespace FightCoachAI.Application.DTOs.User;

public class UserProfileResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Discipline { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TotalVideos { get; set; }
    public int TotalSessions { get; set; }
    public double? AverageFightIq { get; set; }
    public string? HighestGuardScore { get; set; }
    public string? PeakHandSpeed { get; set; }
}

public class UpdateProfileRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class UserSettingsResponse
{
    public string Language { get; set; } = "tr";
    public string Units { get; set; } = "metric";
    public bool AiAnalysisAlerts { get; set; } = true;
    public bool WeeklyReports { get; set; } = true;
    public bool DarkMode { get; set; } = true;
}

public class UpdateSettingsRequest
{
    public string? Language { get; set; }
    public string? Units { get; set; }
    public bool? AiAnalysisAlerts { get; set; }
    public bool? WeeklyReports { get; set; }
    public bool? DarkMode { get; set; }
}
