using Domain.Exceptions;

namespace Domain.ValueObjects;

public record NonNegativeInteger
{
    public int Value { get; }

    public NonNegativeInteger(
        int value)
    {
        if (value < 0)
        {
            throw new InvalidEntityStateException(
                "NonNegativeInteger must be zero or more.");
        }

        Value = value;
    }
}