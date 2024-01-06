using Application.Slices.SoccerFields.Common;
using Application.UnitTests.TestCommon;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Application.UnitTests.Slices.SoccerFields;

[Collection("Run Integration Tests Sequentially")]
public class SoccerFieldRepositoryTests
    : TestWithDatabase
{
    private SoccerFieldRepository CreateSut()
    {
        return new SoccerFieldRepository(TestDbContext);
    }
    
    private static SoccerField CreateSoccerFieldAggregate()
    {
        return new SoccerField(
            EntityIdentity.Random, new FieldLocation(
                UsState.Tennessee, new NonEmptyString("Chattanooga"), new NonEmptyString("Main St"),
                new NonEmptyString("2223"), new NonEmptyString("30420")),
            new NonEmptyString("La Bombonerita"));
    }
    
    [Fact(Skip = TestWithDatabase.SKIP_INTEGRATION_TESTS)]
    public async Task CanStoreAndRetrieve()
    {
        // ************ ARRANGE ************

        var soccerField = CreateSoccerFieldAggregate();

        var sut = CreateSut();

        // ************ ACT ****************

        await sut.AddAsync(soccerField);
        await sut.CommitChangesAsync();
        var result = await sut.LoadAsync(soccerField.Id);

        // ************ ASSERT *************

        var casted = (SoccerField)result;
        
        Assert.Equal(soccerField.Id, casted.Id);
        Assert.Equal(soccerField.Location, casted.Location);
        Assert.Equal(soccerField.Name, casted.Name);
    }
    

    [Fact(Skip = TestWithDatabase.SKIP_INTEGRATION_TESTS)]
    public async Task Load_WhenNoEntityExists_ReturnsNull()
    {
        // ************ ARRANGE ************
        
        var sut = CreateSut();

        // ************ ACT ****************

        var result = await sut.LoadAsync(Guid.NewGuid());

        // ************ ASSERT *************
        
        Assert.Null(result);
    }
}