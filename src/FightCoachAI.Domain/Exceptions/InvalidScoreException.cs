namespace FightCoachAI.Domain.Exceptions;

public class InvalidScoreException : DomainException
{
    public InvalidScoreException(int value)
        : base($"Score must be between 0 and 100. Got: {value}") { }
}
