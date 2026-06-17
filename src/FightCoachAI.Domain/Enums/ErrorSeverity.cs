namespace FightCoachAI.Domain.Enums;

public static class ErrorSeverity
{
    public const string Critical = "Critical";
    public const string Medium = "Medium";
    public const string Low = "Low";

    public static readonly string[] All = [Critical, Medium, Low];
}
