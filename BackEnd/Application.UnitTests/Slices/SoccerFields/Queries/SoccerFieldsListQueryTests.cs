using Application.Slices.SoccerFields.Queries.GetList;
using Application.TestCommon.Extensions;
using Application.UnitTests.TestCommon;
using Application.UnitTests.TestCommon.Factories;

namespace Application.UnitTests.Slices.SoccerFields.Queries;

[Collection("Run Integration Tests Sequentially")]
public class SoccerFieldsListQueryTests
    : TestWithDatabase
{
    private SoccerFieldsListQuery.Handler CreateSut()
    {
        return new (TestDbContext);
    }
    


    [Fact(Skip = TestWithDatabase.SKIP_INTEGRATION_TESTS)]
    public async Task CanGetSoccerFields()
    {

        // ************ ARRANGE ************

        var field1 = SoccerFieldFactory.Create();
        var field2 = SoccerFieldFactory.Create();

        await TestDbContext.SoccerFields.AddRangeAsync(field1, field2);
        await TestDbContext.SaveChangesAsync();

        var sut = CreateSut();
        var query = new SoccerFieldsListQuery();

        // ************ ACT ****************

        var result = await sut.Handle(query,
            CancellationToken.None);

        // ************ ASSERT *************

        Assert.Equal(2, result.Lookups.Count());
        
        result.Lookups.ShouldBeEquivalentTo(
            field1.ToCollection(field2),(x,y)=>
                x.Id == y.Id &&
                x.Name == y.Name);

    }
}