using Domain.AggregatesAndEntities.Calendars.ValueObjects;
using Domain.ValueObjects;

namespace Domain.AggregatesAndEntities.Calendars;

public record Slot(EntityIdentity Id, DayOfWeek Day, NonNegativeInteger Duration, MinutesSinceMidnight Begins,
    FieldRental? Rental)
{
    public MinutesSinceMidnight Ends => Begins.AddMinutes(Duration.Value);
}