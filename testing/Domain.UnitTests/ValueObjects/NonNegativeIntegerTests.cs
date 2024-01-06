using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class NonNegativeIntegerTests
{

    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new NonNegativeInteger(100);

        // ************ ASSERT *************
        
        Assert.Equal(100, result.Value);
    }


    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, false)]
    public void AssertsValueNonNegative(
        int value, bool shouldThrow)
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = Record.Exception(() =>
            new NonNegativeInteger(value));

        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("NonNegativeInteger must be zero or more.",
                result.Message);
        }
        else
        {
            Assert.Null(result);
        }
    }
}