using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Base class for a value object representing a non empty string with a custom user facing exception error message.
/// </summary>
public record NonEmptyString
{
    public string Value { get; }

    public NonEmptyString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEntityStateException(
                "NonEmptyString value is required.");
        }

        Value = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        // Sealed prevents the implementation from changing the expected behavior.
        // Records, by default, override ToString with the type name.
        return Value;
    }
}