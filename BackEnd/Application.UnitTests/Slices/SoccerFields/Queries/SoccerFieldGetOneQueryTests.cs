using Application.Slices.SoccerFields.Queries.GetOne;
using Application.UnitTests.TestCommon;
using Application.UnitTests.TestCommon.Factories;

namespace Application.UnitTests.Slices.SoccerFields.Queries;

[Collection("Run Integration Tests Sequentially")]
public class SoccerFieldGetOneQueryTests : TestWithDatabase
{

    private SoccerFieldGetOneQuery.Handler CreateSut()
    {
        return new(TestDbContext);
    }

    [Fact(Skip = TestWithDatabase.SKIP_INTEGRATION_TESTS)]
    public async Task CanGetOne()
    {

        // ************ ARRANGE ************

        var field = SoccerFieldFactory.Create();
        await TestDbContext.SoccerFields.AddAsync(field);
        await TestDbContext.SaveChangesAsync();

        var sut = CreateSut();
        var query = new SoccerFieldGetOneQuery()
        {
            Id = field.Id
        };
        
        // ************ ACT ****************

        var result = await sut.Handle(query, CancellationToken.None);

        // ************ ASSERT *************
        
        Assert.Equal(field.Id, result.Id);
        Assert.Equal(field.Name, result.Name);
        Assert.Equal(field.Location.ToString(), result.Address);


    }
}