namespace FightCoachAI.Domain.Exceptions;

public class VideoLimitExceededException : DomainException
{
    public VideoLimitExceededException(int current, int limit)
        : base($"Monthly video limit exceeded. Used: {current}/{limit}") { }
}
