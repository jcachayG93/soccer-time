using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Represents the unique identity for a given Domain Driven Design Entity
/// </summary>
public record EntityIdentity
{
    public Guid Value { get; }
    
    public EntityIdentity(
        Guid value)
    {
        AssertNotEmpty(value);
        Value = value;
    }

    private void AssertNotEmpty(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityStateException(
                "An Entity Identity can't be empty.");
        }
    }

    /// <summary>
    /// Creates one with a random value
    /// </summary>
    public static EntityIdentity Random => new EntityIdentity(Guid.NewGuid());

    /// <inheritdoc />
    public override string ToString()
    {
        return Value.ToString();
    }
}