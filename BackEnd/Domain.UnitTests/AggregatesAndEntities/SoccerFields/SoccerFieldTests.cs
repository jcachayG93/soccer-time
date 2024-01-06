using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.UnitTests.AggregatesAndEntities.SoccerFields;

public class SoccerFieldTests
{
    private SoccerField CreateSoccerField()
    {
        var id = EntityIdentity.Random;
        var location = new FieldLocation(
            UsState.Tennessee,
            new NonEmptyString("Chattanooga"),
            new NonEmptyString("Main St."),
            new NonEmptyString("221"),
            new NonEmptyString("37420"));
        var name = new NonEmptyString("La Bombonera");
        
        return new SoccerField(
            id, location, name);
    }
    
    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        var id = EntityIdentity.Random;
        var location = new FieldLocation(
            UsState.Tennessee,
            new NonEmptyString("Chattanooga"),
            new NonEmptyString("Main St."),
            new NonEmptyString("221"),
            new NonEmptyString("37420"));
        var name = new NonEmptyString("La Bombonera");

        // ************ ACT ****************

        var result = new SoccerField(
            id, location, name);

        // ************ ASSERT *************
        
        Assert.Equal(id.Value, result.Id);
        Assert.Equal(location, result.Location);
        Assert.Equal(name.Value, result.Name);
    }


    [Fact]
    public void CanUpdateName()
    {
        // ************ ARRANGE ************

        var sut = CreateSoccerField();

        // ************ ACT ****************
        
        sut.UpdateName("El Monumental");

        // ************ ASSERT *************
        
        Assert.Equal("El Monumental", sut.Name);
    }
}