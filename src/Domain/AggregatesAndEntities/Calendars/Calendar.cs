using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.AggregatesAndEntities.Calendars;

/// <summary>
/// A calendar for one soccer field, for one period of time that defines
/// the slots available for renting and the clients who rented them.
/// </summary>
public class Calendar
{
    
    public Guid Id { get; private set; }

    /// <summary>
    /// A calendar must be attached to one particular soccer field
    /// </summary>
    public Guid SoccerFieldId { get; private set; }
    
    /// <summary>
    /// The date from which this calendar starts
    /// </summary>
    public MidnightUtcDate From { get; private set; }
    
    /// <summary>
    /// The date from which this calendar ends
    /// </summary>
    public MidnightUtcDate To { get; private set; }

    /// <summary>
    /// The time at which this calendar starts (minutes since midnight)
    /// </summary>
    public MinutesSinceMidnight Begins { get; private set; }
    
    /// <summary>
    /// The time between time slots, so there is some preparation time between
    /// games (we don't one the time when one client starts to be at the same time
    /// when another ends)
    /// </summary>
    public int TimeBetweenSlots { get; private set; }

    // I want a list so the domain can add/remove things easily, but want to encapsulate to the
    // client (which is in the Application project), you can also do this with backing fields but that requires
    // magic strings, I prefer to do it this way (Internal access with an P in front)
    internal List<CalendarSlot> PCalendarSlots { get; } = new();
    
    /*
     * Note CalendarSlots is an abstraction for the entity PCalendarSlots.
     * It is not stored in the database, but rather lazily calculated each time is
     * read. This way we can include information that comes from calculations like the
     * Begins and Ends values.
     */
    /// <summary>
    /// The time slots that can be rent to clients
    /// </summary>
    public IEnumerable<Slot> CalendarSlots
    {
        get
        {
            var dates = To.GetDayDatesSince(From);

            return dates.SelectMany(d =>
                PCalendarSlots
                    .Where(slot => slot.Date == d)
                    .OrderBy(slot => slot.Index)
                    .Select(slot => new Slot(
                        new EntityIdentity(slot.Id),
                        d.DayOfWeek,
                        slot.Duration,
                        CalculateSlotBegins(slot.Id)))
                ).ToArray();
        }
    }

    private MinutesSinceMidnight CalculateSlotBegins(Guid slotId)
    {
        var matchingSlot = PCalendarSlots.First(x =>
            x.Id == slotId);

        var beforeMatchingSlotInDay = PCalendarSlots
            .Where(x => x.Date == matchingSlot.Date &&
                        x.Index < matchingSlot.Index);

        var result = Begins.Value + beforeMatchingSlotInDay
            .Sum(s => s.Duration.Value + TimeBetweenSlots);

        return new MinutesSinceMidnight(result);
    }

    // Ef Core needs this
    private Calendar()
    {
       
    }
    public Calendar(
        EntityIdentity id, 
        EntityIdentity soccerFieldId,
        MidnightUtcDate from, 
        MidnightUtcDate to, 
        MinutesSinceMidnight begins, 
        NonNegativeInteger timeBetweenSlots,
        NonNegativeInteger slotDuration, 
        NonNegativeInteger slotsPerDay)
    {
        PCalendarSlots = CreateSlots(from, to, slotDuration, slotsPerDay).ToList();

        Id = id.Value;
        SoccerFieldId = soccerFieldId.Value;
        From = from;
        To = to;
        Begins = begins;
        TimeBetweenSlots = timeBetweenSlots.Value;
        
        AssertEntityStateIsValid();
    }

    /*
     * Marking methods as static makes it clear that the method is pure, does not use object data.
     */
    private static IEnumerable<CalendarSlot> CreateSlots(
        MidnightUtcDate from, MidnightUtcDate to, 
        NonNegativeInteger slotDuration, NonNegativeInteger slotsPerDay)
    {
        var dates = to.GetDayDatesSince(from);

        var result = new List<CalendarSlot>();

        foreach (var date in dates)
        {
            var dateSlots = Enumerable.Range(0, slotsPerDay.Value)
                .Select(i => new CalendarSlot(EntityIdentity.Random, new NonNegativeInteger(i), date, slotDuration))
                .ToList();
            
            result.AddRange(dateSlots);
        }

        return result;
    }

    public void AssertEntityStateIsValid()
    {
        // To must be at least one day after from

        if ((To.Value - From.Value).Days < 1)
        {
            throw new InvalidEntityStateException("Calendar To must come after From (at least one day apart).");
        }
        
        // Assert that no day has slots that would cause it to last more than 1439 minutes (midnight)
        var dates = To.GetDayDatesSince(From);
        foreach (var currentDate in dates)
        {
            if (CalculateLastSlotWouldEndInMinutesSinceMidnight(currentDate) > 1439)
            {
                throw new InvalidEntityStateException("Calendar day must end before midnight.");
            }
        }
        AssertEntityStateIsValidWasCalled = true;
    }

    private int CalculateLastSlotWouldEndInMinutesSinceMidnight(MidnightUtcDate date)
    {
        var result = Begins.Value;
        var slots = PCalendarSlots.Where(s => s.Date == date).ToArray();
        result += slots.Sum(x => x.Duration.Value + TimeBetweenSlots);

        return result;
    }

    internal bool AssertEntityStateIsValidWasCalled { get; set; } = false;
}