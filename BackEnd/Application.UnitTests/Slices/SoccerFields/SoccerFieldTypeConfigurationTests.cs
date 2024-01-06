using Application.UnitTests.TestCommon;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Application.UnitTests.Slices.SoccerFields;

[Collection("Run Integration Tests Sequentially")]
public class SoccerFieldTypeConfigurationTests
: TestWithDatabase
{
    [Fact(Skip = TestWithDatabase.SKIP_INTEGRATION_TESTS)]
    public void CanStoreAndRetrieve()
    {
        // ************ ARRANGE ************

        var soccerField = new SoccerField(
            EntityIdentity.Random, new FieldLocation(
                UsState.Tennessee, new NonEmptyString("Chattanooga"), new NonEmptyString("Main St"),
                new NonEmptyString("2223"), new NonEmptyString("30420")),
            new NonEmptyString("La Bombonerita"));

        // ************ ACT ****************

        TestDbContext.SoccerFields.Add(soccerField);
        TestDbContext.SaveChanges();
        var result = TestDbContext.Find<SoccerField>(soccerField.Id);

        // ************ ASSERT *************
        
        Assert.Equal(soccerField.Id, result.Id);
        Assert.Equal(soccerField.Location, result.Location);
        Assert.Equal(soccerField.Name, result.Name);
    }
}