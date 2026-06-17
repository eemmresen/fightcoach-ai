using System.Text.RegularExpressions;

namespace FightCoachAI.Domain.ValueObjects;

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException($"Invalid email format: {value}");

        Value = value.ToLowerInvariant();
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is Email other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
