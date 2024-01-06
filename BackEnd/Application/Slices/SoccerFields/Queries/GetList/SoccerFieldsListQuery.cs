using Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
// ReSharper disable MethodSupportsCancellation

namespace Application.Slices.SoccerFields.Queries.GetList;

/// <summary>
/// Gets a list of all soccer fields
/// </summary>
public class SoccerFieldsListQuery
: IRequest<SoccerFieldsListViewModel>
{

    public class Handler : IRequestHandler<SoccerFieldsListQuery,
        SoccerFieldsListViewModel>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <inheritdoc />
        public async Task<SoccerFieldsListViewModel> Handle(
            SoccerFieldsListQuery request, 
            CancellationToken cancellationToken)
        {
            var lookups = await _dbContext.SoccerFields
                .Select(field =>
                    new SoccerFieldsListViewModel.SoccerFieldsListLookup
                    {
                        Id = field.Id,
                        Name = field.Name
                    }).ToListAsync();

            return new SoccerFieldsListViewModel()
            {
                Lookups = lookups
            };
        }
    }
}