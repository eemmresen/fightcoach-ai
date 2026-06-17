namespace FightCoachAI.Domain.Enums;

public static class VideoStatus
{
    public const string Uploaded = "Uploaded";
    public const string Processing = "Processing";
    public const string Completed = "Completed";
    public const string Failed = "Failed";

    public static readonly string[] All = [Uploaded, Processing, Completed, Failed];
}
