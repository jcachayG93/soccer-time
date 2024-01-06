using Domain.Exceptions;
using Domain.ValueObjects;
// ReSharper disable ObjectCreationAsStatement
#pragma warning disable CS8604 // Possible null reference argument.

namespace Domain.UnitTests.ValueObjects;

public class NonEmptyStringBaseTests
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
            new Sut(value);
        });

        // ************ ASSERT *************

        if (shouldThrow)
        {
            Assert.NotNull(result);
            Assert.IsType<DomainUserFacingException>(result);
            Assert.Equal("Custom error message when value is empty or whitespace.", result.Message);
        }
    }


    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new Sut("Hello world!");

        // ************ ASSERT *************
        
        Assert.Equal("Hello world!", result.Value);
    }


    [Fact]
    public void ToString_ReturnsValue()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = new Sut("Hello world!");

        // ************ ASSERT *************
        
        Assert.Equal("Hello world!", result.ToString());
    }
    
}

record Sut : NonEmptyStringBase
{
    /// <inheritdoc />
    public Sut(string value) : base(value, "Custom error message when value is empty or whitespace.")
    {
    }
}