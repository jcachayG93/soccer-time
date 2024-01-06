using Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class UsStateTests
{

    [Fact]
    public void CanCreateTennessee()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var tennessee = UsState.Tennessee;

        // ************ ASSERT *************
        
        Assert.Equal("Tennessee", tennessee.Name);
        Assert.Equal("TN", tennessee.Abbreviation);
    }


    [Fact]
    public void CanCreateGeorgia()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var georgia = UsState.Georgia;

        // ************ ASSERT *************
        
        Assert.Equal("Georgia", georgia.Name);
        Assert.Equal("GA", georgia.Abbreviation);
    }


    [Fact]
    public void ToString_ForTennessee_ReturnsTennessee()
    {
        // ************ ARRANGE ************

        var tennesee = UsState.Tennessee;
        
        // ************ ACT ****************

        var result = tennesee.ToString();
        
        // ************ ASSERT *************
        
        Assert.Equal(tennesee.Name, result.ToString());
    }
    
}