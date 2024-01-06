using Domain.Aggregates;

namespace Application.Slices.SoccerFields.Common;

public interface ISoccerFieldRepository
{
    Task AddAsync(SoccerField soccerField);

    Task<ISoccerFieldAggregateRoot?> LoadAsync(Guid id);

    Task<int> CommitChangesAsync();
}