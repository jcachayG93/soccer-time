using Domain.ValueObjects;

namespace Domain.AggregatesAndEntities.Calendars;

public class CalendarSlot
{
    public Guid Id { get; private set; }

    /// <summary>
    /// The Index value is intended to make it explicit that the order for the CalendarSlots is important.
    /// </summary>
    public int Index { get; }
    public MidnightUtcDate Date { get; private set; }

    public NonNegativeInteger Duration { get; private set; }

    public CalendarSlot(EntityIdentity id, NonNegativeInteger index, MidnightUtcDate date, NonNegativeInteger duration)
    {
        Id = id.Value;
        Date = date;
        Duration = duration;
        Index = index.Value;
    }
}