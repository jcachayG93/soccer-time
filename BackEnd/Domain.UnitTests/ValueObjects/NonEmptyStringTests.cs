using Domain.Exceptions;
using Domain.ValueObjects;
// ReSharper disable ObjectCreationAsStatement
#pragma warning disable CS8604 // Possible null reference argument.

namespace Domain.UnitTests.ValueObjects;

public class NonEmptyStringTests
{


    [Theory]
    [InlineData("Some value", false)]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("   ", true)]
    public void AssertsValueNonEmpty(
        string? value, bool shouldThrow)
    {
        // ************ ARRANGE ************
        
        
        // ************ ACT ****************

        var result = Record.Exception(() =>
        {
            // For a value object, I prefer not to rely on Nullable reference types, so I am ignoring them.
            new NonEmptyString(value);
        });

        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<InvalidEntityStateException>(result);
            Assert.Equal("NonEmptyString value is required.", result.Message);
        }
    }


    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new NonEmptyString("Hello world!");

        // ************ ASSERT *************
        
        Assert.Equal("Hello world!", result.Value);
    }


    [Fact]
    public void ToString_ReturnsValue()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new NonEmptyString("Hello world!");

        // ************ ASSERT *************
        
        Assert.Equal("Hello world!", result.ToString());
    }
    
}

