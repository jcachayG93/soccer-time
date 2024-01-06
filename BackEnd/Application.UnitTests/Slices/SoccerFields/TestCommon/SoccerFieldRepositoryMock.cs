using Application.Slices.SoccerFields.Common;
using Domain.Aggregates;
using Moq;

namespace Application.UnitTests.Slices.SoccerFields.TestCommon;


internal class SoccerFieldRepositoryMock
{
    public SoccerFieldRepositoryMock()
    {
        _moq = new();
        LoadReturns = new();
        _moq.Setup(x => x.LoadAsync(It.IsAny<Guid>())
            .Result).Returns(LoadReturns.Object);
    }

    private readonly Mock<ISoccerFieldRepository> _moq;

    public ISoccerFieldRepository Object => _moq.Object;

    public Mock<ISoccerFieldAggregateRoot> LoadReturns { get; }
    public void VerifyAdd(
        Func<SoccerField, bool> predicate)
    {
        _moq.Verify(x=>x.AddAsync(
            It.Is<SoccerField>(x=>predicate(x))));
    }

    public void VerifyLoad(Guid id)
    {
        _moq.Verify(x=>x.LoadAsync(id));
    }

    public void VerifyCommitChanges()
    {
        _moq.Verify(x=>x.CommitChangesAsync());
    }

    public void SetupLoad(out Mock<ISoccerFieldAggregateRoot> returns)
    {
        returns = new();
        
        SetupLoad(returns.Object);
    }

    public void SetupLoadReturnsNull()
    {
        SetupLoad(null);
    }

    private void SetupLoad(ISoccerFieldAggregateRoot? returns)
    {
        _moq.Setup(x => x.LoadAsync(It.IsAny<Guid>()).Result)
            .Returns(returns);
    }
}