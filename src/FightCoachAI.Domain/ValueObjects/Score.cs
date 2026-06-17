namespace FightCoachAI.Domain.ValueObjects;

public class Score
{
    public int Value { get; }

    public Score(int value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), $"Score must be between 0 and 100. Got: {value}");

        Value = value;
    }

    public static implicit operator int(Score score) => score.Value;
    public static explicit operator Score(int value) => new(value);

    public override string ToString() => Value.ToString();
    public override bool Equals(object? obj) => obj is Score other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
