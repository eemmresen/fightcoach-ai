namespace FightCoachAI.Domain.Enums;

public static class AchievementType
{
    public const string SessionStreak = "SessionStreak";
    public const string GuardScore = "GuardScore";
    public const string JabCount = "JabCount";
    public const string ComboDiversity = "ComboDiversity";
    public const string FootworkMastery = "FootworkMastery";
    public const string FightIqMilestone = "FightIqMilestone";
    public const string EarlyAdopter = "EarlyAdopter";
    public const string SpeedDemon = "SpeedDemon";

    public static readonly string[] All = [SessionStreak, GuardScore, JabCount, ComboDiversity, FootworkMastery, FightIqMilestone, EarlyAdopter, SpeedDemon];
}
