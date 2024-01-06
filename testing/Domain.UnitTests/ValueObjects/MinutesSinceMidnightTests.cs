using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class MinutesSinceMidnightTests
{

    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new MinutesSinceMidnight(100);

        // ************ ASSERT *************
        
        Assert.Equal(100, result.Value);
    }


    [Theory]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    [InlineData(1439, false)]
    [InlineData(1440, true)]
    public void AssertsEndsBeforeMidnight(
        int value, bool shouldThrow)
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = Record.Exception(() =>
            new MinutesSinceMidnight(value));

        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("MinutesSinceMidnigth value must be between 0 and 1439.", 
                result.Message);
        }
        else
        {
            Assert.Null(result);
        }
    }
}