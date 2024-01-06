using Domain.ValueObjects;

namespace Domain.AggregatesAndEntities.Calendars.ValueObjects;

/// <summary>
/// When a client rents a soccer field
/// </summary>
public record FieldRental(EntityIdentity ClientId);