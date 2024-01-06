using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Wraps a DateTime object, enforcing UTC formatting and settings the time to the
/// start of the day (12 am: Midnight)
/// </summary>
public record MidnightUtcDate
{
    public DateTime Value { get; }
    
    public MidnightUtcDate(DateTime value)
    {
        // https://stackoverflow.com/a/13467254/14132160
        Value = value.Date;
        
        if (Value.Kind != DateTimeKind.Utc)
        {
            throw new InvalidEntityStateException("Date value must be UTC.");
        }
    }
}