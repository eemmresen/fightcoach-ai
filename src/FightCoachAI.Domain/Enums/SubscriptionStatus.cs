namespace FightCoachAI.Domain.Enums;

public static class SubscriptionStatus
{
    public const string Active = "Active";
    public const string Cancelled = "Cancelled";
    public const string Expired = "Expired";

    public static readonly string[] All = [Active, Cancelled, Expired];
}
