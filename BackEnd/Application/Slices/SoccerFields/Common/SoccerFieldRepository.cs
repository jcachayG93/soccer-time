using Application.Persistence;
using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Application.Slices.SoccerFields.Common;

public class SoccerFieldRepository : ISoccerFieldRepository
{
    private readonly AppDbContext _dbContext;

    public SoccerFieldRepository(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task AddAsync(SoccerField soccerField)
    {
        await _dbContext.AddAsync(soccerField);
    }

    /// <inheritdoc />
    public async Task<ISoccerFieldAggregateRoot?> LoadAsync(Guid id)
    {
        return await _dbContext.SoccerFields.FirstOrDefaultAsync(s =>
            s.Id == id);
    }

    /// <inheritdoc />
    public Task<int> CommitChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}