using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class FieldLocationTests
{
    /*
     * The reason to use a factory here is avoid having too many
     * references to the constructor of the value object to
     * facilitate refactoring.
     */
    private NonEmptyString CreateNonEmptyString(
        string value)
    {
        return new NonEmptyString(value);
    }

    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        /*
         * I like using real examples for testing in the domain
         * layer, that way the tests can better document the code.
         */
        var state = UsState.Tennessee;
        var city = new NonEmptyString("Chattanooga");
        var street = CreateNonEmptyString("Main St.");
        var number = CreateNonEmptyString("2221");
        var zipCode = CreateNonEmptyString("37401");

        // ************ ACT ****************

        var sut = new FieldLocation(
            UsState.Tennessee,
            city, street, number, zipCode);

        // ************ ASSERT *************
        
        Assert.Equal(state.Name, sut.StateName);
        Assert.Equal(city.Value, sut.City);
        Assert.Equal(street.Value, sut.Street);
        Assert.Equal(number.Value, sut.Number);
        Assert.Equal(zipCode.Value, sut.ZipCode);
    }

    [Fact]
    public void ToString_ReturnsFormattedAddress()
    {
        // ************ ARRANGE ************
        
        /*
         * I like using real examples for testing in the domain
         * layer, that way the tests can better document the code.
         */

        var state = UsState.Tennessee;
        var city = new NonEmptyString("Chattanooga");
        var street = CreateNonEmptyString("Main St.");
        var number = CreateNonEmptyString("2221");
        var zipCode = CreateNonEmptyString("37401");
        
        var sut = new FieldLocation(
            state, city, street, number, zipCode);

        // ************ ACT ****************

        var result = sut.ToString();

        // ************ ASSERT *************
        
        Assert.Equal("2221 Main St. Chattanooga, Tennessee. 37401",
            result.ToString());
    }
}