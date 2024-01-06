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
}