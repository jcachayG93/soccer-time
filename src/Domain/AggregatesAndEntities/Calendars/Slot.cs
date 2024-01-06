using Domain.ValueObjects;

namespace Domain.AggregatesAndEntities.Calendars;

public record Slot(EntityIdentity Id, DayOfWeek Day, NonNegativeInteger Duration, MinutesSinceMidnight Begins)
{
    public MinutesSinceMidnight Ends => Begins.AddMinutes(Duration.Value);
}