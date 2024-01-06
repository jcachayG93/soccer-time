using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class EntityIdentityTests
{

    [Fact]
    public void AssertsValueNotEmpty()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = Record.Exception(() => new EntityIdentity(Guid.Empty));
        
        // ************ ASSERT *************
        
        Assert.NotNull(result);
        Assert.IsType<InvalidEntityStateException>(result);
        Assert.Equal("An Entity Identity can't be empty.", result.Message);
    }


    [Fact]
    public void CanCreate()
    {
        // ************ ARRANGE ************

        var value = Guid.NewGuid();
        
        // ************ ACT ****************

        var sut = new EntityIdentity(value);

        // ************ ASSERT *************
        
        Assert.Equal(value, sut.Value);
    }


    [Fact]
    public void CanCreateWithRandomValue()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var result = EntityIdentity.Random;

        // ************ ASSERT *************
        
        Assert.NotEqual(Guid.NewGuid(), result.Value);
    }


    [Fact]
    public void ToString_ReturnsValue()
    {
        // ************ ARRANGE ************

        var value = Guid.NewGuid();

        // ************ ACT ****************

        var result = new EntityIdentity(value);

        // ************ ASSERT *************
        
        Assert.Equal(value.ToString(), result.ToString());
    }
}