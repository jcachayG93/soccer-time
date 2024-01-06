
using Domain.AggregatesAndEntities.Calendars;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.AggregatesAndEntities.Calendars;

public class CalendarTests
{
    /*
     * Having method to create instances reduces the number of references to the aggregate constructor, facilitating
     * refactoring.
     */
    private Calendar CreateSut(
        EntityIdentity id, EntityIdentity soccerFieldId, MidnightUtcDate from, MidnightUtcDate to, MinutesSinceMidnight begins,
        NonNegativeInteger timeBetweenSlots, NonNegativeInteger slotDuration, NonNegativeInteger slotsPerDay)
    {
        var result = new Calendar(id, soccerFieldId, from, to, begins, timeBetweenSlots, slotDuration, slotsPerDay);
        result.AssertEntityStateIsValidWasCalled = false;

        return result;
    }
    
    [Fact]
    public void Constructor_SetsRootValues_AssertInvariants()
    {
        // ************ ARRANGE ************

        var id = EntityIdentity.Random;
        var soccerFieldId = EntityIdentity.Random;
        var from = MidnightUtcDate.Now;
        var to = from.AddDays(30);

        // ************ ACT ****************

        var sut = new Calendar(
            id, soccerFieldId, from, to, new MinutesSinceMidnight(480), new NonNegativeInteger(5),
            new NonNegativeInteger(45), new NonNegativeInteger(8));

        // ************ ASSERT *************
        
        Assert.Equal(id.Value, sut.Id);
        Assert.Equal(soccerFieldId.Value, sut.SoccerFieldId);
        Assert.Equal(from, sut.From);
        Assert.Equal(to, sut.To);
        Assert.Equal(5, sut.TimeBetweenSlots);
        
        Assert.True(sut.AssertEntityStateIsValidWasCalled);
    }

    [Fact]
    public void Constructor_InitializesSlotsAsSpecified_AssertsInvariants()
    {
        // ************ ARRANGE ************

        var from = new MidnightUtcDate(new DateTime(2024, 1, 1).ToUniversalTime()); // Monday
        var to = new MidnightUtcDate(new DateTime(2024, 1, 5).ToUniversalTime()); // Friday
        var begins = new MinutesSinceMidnight(480); // Opens 8 am.
        var duration = new NonNegativeInteger(45); // Each slot lasts 45 minutes.
        var timeBetweenSlots = new NonNegativeInteger(15); // There must be a 15 minutes between slots so teams can change.
        var slotsPerDay = new NonNegativeInteger(5); // There are 5 slots per day.
        
        /*
         * For Any one day:
         * - 5 slots: 8:00am - 8:45am, 9:00 am - 9:45 am, 10:00 am - 10:45 am, 11:00 am, 11:45 am, 12:00 pm - 12:45 pm.
         * - The calendar starts on a monday and ends on a friday. So it has 5 days x 5 slots = 25 slots.
         */
        
        // ************ ACT ****************

        var sut = new Calendar(EntityIdentity.Random, EntityIdentity.Random,
            from, to, begins, timeBetweenSlots, duration, slotsPerDay);

        // ************ ASSERT *************
        
        Assert.Equal(25, sut.PCalendarSlots.Count);
        Assert.True(sut.PCalendarSlots.All(x=>x.Duration == duration));
        
        var mondayThruFriday = Enumerable.Range((int)DayOfWeek.Monday, 5);

        foreach (var day in mondayThruFriday)
        {
            var slotsForDay = sut.PCalendarSlots.Where(x =>
                x.Date.DayOfWeek == (DayOfWeek) day).ToArray();
            
            Assert.Equal(5, slotsForDay.Length);
            
            // Slots Index indicates the order for each slot in  a day
            var currentIndex = 0;
            foreach (var slot in slotsForDay)
            {
                Assert.Equal(currentIndex, slot.Index);
                currentIndex++;
            }
            
        }
        
        Assert.True(sut.AssertEntityStateIsValidWasCalled);
        
        
    }

    [Fact]
    public void CalendarSlots_AbstractsUnderlyingCalendarSlotEntity_WithCorrectBeginsAndEndsValues()
    {
        // ************ ARRANGE ************
        

        var from = new MidnightUtcDate(new DateTime(2024, 1, 1).ToUniversalTime()); // Monday
        var to = new MidnightUtcDate(new DateTime(2024, 1, 5).ToUniversalTime()); // Friday
        var begins = new MinutesSinceMidnight(480); // Opens 8 am.
        var duration = new NonNegativeInteger(45); // Each slot lasts 45 minutes.
        var timeBetweenSlots = new NonNegativeInteger(15); // There must be a 15 minutes between slots so teams can change.
        var slotsPerDay = new NonNegativeInteger(5); // There are 5 slots per day.
        
        /*
         * For Any one day:
         * - 5 slots: 8:00am - 8:45am, 9:00 am - 9:45 am, 10:00 am - 10:45 am, 11:00 am, 11:45 am, 12:00 pm - 12:45 pm.
         * - The calendar starts on a monday and ends on a friday. So it has 5 days x 5 slots = 25 slots.
         */
        
        // ************ ACT ****************

        var sut = CreateSut(EntityIdentity.Random, EntityIdentity.Random,
            from, to, begins, timeBetweenSlots, duration, slotsPerDay);

        // ************ ASSERT *************
        
        Assert.Equal(25, sut.CalendarSlots.Count());
        Assert.True(sut.CalendarSlots.All(x=>x.Duration == duration));
        
        var mondayThruFriday = Enumerable.Range((int)DayOfWeek.Monday, 5);

        var slotsByDay = sut.CalendarSlots.GroupBy(x => x.Day);

        var expectedSlotTimes = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(480, 525), // 8:00 am - 8:45 am.
            new Tuple<int, int>(540, 585), // 9:00 am - 9:45 am.
            new Tuple<int, int>(600, 645), // 10:00 am - 10:45 am.
            new Tuple<int, int>(660, 705), // 11:00 am - 11:45 am.
            new Tuple<int, int>(720, 765) // 12:00 am - 12:45 am.
        };
        
        foreach (var group in slotsByDay)
        {
            var day = group.Key;
            var slots = group.ToArray();

            var currentIndex = 0;
            foreach (var expected in expectedSlotTimes)
            {
                var match = slots[currentIndex];
                
                Assert.Equal(expected.Item1, match.Begins.Value);
                Assert.Equal(expected.Item2, match.Ends.Value);
                
                currentIndex++;
            }

        }
    }
 
    
    /*
     * INVARIANTS
     */

    [Theory]
    [InlineData(1439, false)]
    [InlineData(1440, true)]
    public void DayMustEndBeforeMidnight(
        int wouldEndMinutesSinceMidnight, bool shouldThrow)
    {
        // ************ ARRANGE ************

        var begins = new MinutesSinceMidnight(1400);
        var timeBetweenSlots = new NonNegativeInteger(0);
        var slotDuration = new NonNegativeInteger(wouldEndMinutesSinceMidnight - 1400);

        // ************ ACT ****************

        var result = Record.Exception(() =>
        {
            CreateSut(EntityIdentity.Random, EntityIdentity.Random,
                MidnightUtcDate.Now, MidnightUtcDate.Now.AddDays(1), begins, timeBetweenSlots,
                slotDuration, new NonNegativeInteger(1));
        });

        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("Calendar day must end before midnight.", result.Message);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData(1,2,false)]
    [InlineData(1,1, true)]
    [InlineData(2,1, true)]
    public void ToMustComeAfterFrom(
        int fromDay, int toDay, bool shouldThrow)
    {
        // ************ ARRANGE ************

        var from = new MidnightUtcDate(new DateTime(2024, 1, fromDay).ToUniversalTime());
        var to = new MidnightUtcDate(new DateTime(2024, 1, toDay).ToUniversalTime());

        // ************ ACT ****************

        var result = Record.Exception(() =>
        {
            CreateSut(
                EntityIdentity.Random,
                EntityIdentity.Random,
                from,
                to,
                new MinutesSinceMidnight(480),
                new NonNegativeInteger(5),
                new NonNegativeInteger(45),
                new NonNegativeInteger(1));
        });

        // ************ ASSERT *************
        
        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("Calendar To must come after From (at least one day apart).", result.Message);
        }
        else
        {
            Assert.Null(result);
        }
    }
}