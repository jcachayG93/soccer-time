using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class MidnightUtcDateTests
{
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void AssertsValueIsUtc(
        bool isUtc, bool shouldThrow)
    {
        // ************ ARRANGE ************

        var value = isUtc ? DateTime.UtcNow : DateTime.Now;
        
        // ************ ACT ****************

        var result = Record.Exception(() =>
            new MidnightUtcDate(value));
        
        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("Date value must be UTC.", result.Message);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Fact]
    public void ValueIsInputStartingAtMidnight()
    {
        // ************ ARRANGE ************

        var value = DateTime.UtcNow; 

        // ************ ACT ****************

        var result = new MidnightUtcDate(value);

        // ************ ASSERT *************
        
        Assert.Equal(value.Year, result.Value.Year);
        Assert.Equal(value.Month, result.Value.Month);
        Assert.Equal(value.Day, result.Value.Day);
        
        Assert.Equal(0, result.Value.Hour);
        Assert.Equal(0, result.Value.Minute);
        Assert.Equal(0, result.Value.Second);
        Assert.Equal(0, result.Value.Millisecond);
    }
    
    [Fact]
    public void ExposesDayOfWeek()
    {
        // ************ ARRANGE ************

        var value = DateTime.UtcNow; 

        // ************ ACT ****************

        var result = new MidnightUtcDate(value);

        // ************ ASSERT *************
        
        Assert.Equal(value.DayOfWeek, result.DayOfWeek);
    }

    [Fact]
    public void CanCreateNow()
    {
        // ************ ARRANGE ************
        
        

        // ************ ACT ****************

        var result = MidnightUtcDate.Now;

        // ************ ASSERT *************
        
        // https://stackoverflow.com/a/13467254/14132160
        Assert.Equal(DateTime.UtcNow.Date, result.Value);
    }

    [Fact]
    public void CanAddDays()
    {
        // ************ ARRANGE ************

        var now = new MidnightUtcDate(new DateTime(2024, 1, 1).ToUniversalTime());

        // ************ ACT ****************

        var tomorrow = now.AddDays(1);

        // ************ ASSERT *************
        
        Assert.Equal(now.Value.Day + 1, tomorrow.Value.Day);
    }

    [Fact]
    public void CanGetDayDatesFromAnotherDate()
    {
        // ************ ARRANGE ************

        var now = new MidnightUtcDate(new DateTime(2024, 1, 10).ToUniversalTime());
        var twoDaysAgo = new MidnightUtcDate(new DateTime(2024, 1, 8).ToUniversalTime());

        // ************ ACT ****************

        var result = now.GetDayDatesSince(twoDaysAgo);

        // ************ ASSERT *************
        
        Assert.Equal(3, result.Length);
        Assert.Equal(new DateTime(2024,1,8).ToUniversalTime().Date, result[0].Value);
        Assert.Equal(new DateTime(2024,1,9).ToUniversalTime().Date, result[1].Value);
        Assert.Equal(new DateTime(2024,1,10).ToUniversalTime().Date, result[2].Value);
    }
}