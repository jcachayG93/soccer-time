using Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Slices.SoccerFields.Queries.GetOne;

public class SoccerFieldGetOneQuery : IRequest<SoccerFieldViewModel>
{
    public required Guid Id { get; init; }
    
    public class Handler : IRequestHandler<SoccerFieldGetOneQuery,
        SoccerFieldViewModel>
    {
        private readonly AppDbContext _dbContext;

        public Handler(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <inheritdoc />
        public async Task<SoccerFieldViewModel> Handle(SoccerFieldGetOneQuery request,
            CancellationToken cancellationToken)
        {
            // TODO: For some reason, when I use AsNoTracking here I get null. Maybe a bug with ef core?
            var entity = await _dbContext.SoccerFields
                .FirstAsync(e =>
                e.Id == request.Id);

            return new SoccerFieldViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Location.ToString()
            };
        }
    }
}