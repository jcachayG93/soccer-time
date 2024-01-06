using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Base class for a value object representing a non empty string with a custom user facing exception error message.
/// </summary>
public abstract record NonEmptyStringBase
{
    public string Value { get; }

    protected NonEmptyStringBase(string value,
        string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainUserFacingException(errorMessage);
        }

        Value = value;
    }

    /// <inheritdoc />
    public sealed override string ToString()
    {
        // Sealed prevents the implementation from changing the expected behavior.
        // Records, by default, override ToString with the type name.
        return Value;
    }
}