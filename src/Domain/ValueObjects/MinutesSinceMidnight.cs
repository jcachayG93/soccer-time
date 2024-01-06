using Domain.Exceptions;

namespace Domain.ValueObjects;

public record MinutesSinceMidnight
{
    public int Value { get; }

    public MinutesSinceMidnight(
        int value)
    {
        if (value < 0 || value > 1439)
        {
            throw new InvalidEntityStateException(
                "MinutesSinceMidnigth value must be between 0 and 1439.");
        }

        Value = value;
    }
}