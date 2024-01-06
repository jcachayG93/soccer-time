using Application.Persistence;
using Domain.Aggregates;

namespace Application.Slices.SoccerFields.Common;

public interface ISoccerFieldRepository
{
    Task AddAsync(SoccerField soccerField);

    Task<ISoccerFieldAggregateRoot> LoadAsync(Guid id);

    Task<int> CommitChangesAsync();
}

public class SoccerFieldRepository : ISoccerFieldRepository
{
    private readonly AppDbContext _dbContext;

    public SoccerFieldRepository(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public Task AddAsync(SoccerField soccerField)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<ISoccerFieldAggregateRoot> LoadAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> CommitChangesAsync()
    {
        throw new NotImplementedException();
    }
}