using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Wraps a DateTime object, enforcing UTC formatting and settings the time to the
/// start of the day (12 am: Midnight)
/// </summary>
public record MidnightUtcDate
{
    public DateTime Value { get; }

    public DayOfWeek DayOfWeek => Value.DayOfWeek;
    public MidnightUtcDate(DateTime value)
    {
        // https://stackoverflow.com/a/13467254/14132160
        Value = value.Date;
        
        if (Value.Kind != DateTimeKind.Utc)
        {
            throw new InvalidEntityStateException("Date value must be UTC.");
        }
    }

    public static MidnightUtcDate Now => new MidnightUtcDate(DateTime.UtcNow);

    public MidnightUtcDate AddDays(int numberOfDays)
    {
        return new MidnightUtcDate(Value.AddDays(numberOfDays));
    }

    /// <summary>
    /// Gets the date for each day between this and a past date. Includes the past date and this date in the result.
    /// When pastDate is not in the past, returns an empty array.
    /// </summary>
    public MidnightUtcDate[] GetDayDatesSince(MidnightUtcDate pastDate)
    {
        if (pastDate == this)
        {
            return Array.Empty<MidnightUtcDate>();
        }
        
        return Enumerable.Range(0, 1 + Value.Subtract(pastDate.Value).Days)
            .Select(offset => pastDate.Value.AddDays(offset))
            .Select(d=>new MidnightUtcDate(d))
            .ToArray(); 
    }
}