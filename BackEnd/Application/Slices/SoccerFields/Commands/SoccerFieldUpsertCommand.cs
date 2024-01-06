using Application.Slices.SoccerFields.Common;
using Domain.Aggregates;
using Domain.ValueObjects;
using MediatR;

namespace Application.Slices.SoccerFields.Commands;

public class SoccerFieldUpsertCommand
: IRequest
{
    public required Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Location_StateName { get; set; }
    
    public required string Location_City { get; set; }
    
    public required string Location_Street { get; set; }
    
    public required string Location_Number { get; set; }
    
    public required string Location_ZipCode { get; set; }

    public class Handler : IRequestHandler<SoccerFieldUpsertCommand>
    {
        private readonly ISoccerFieldRepository _repository;

        public Handler(
            ISoccerFieldRepository repository)
        {
            _repository = repository;
        }
        
        /// <inheritdoc />
        public async Task Handle(SoccerFieldUpsertCommand request,
            CancellationToken cancellationToken)
        {
            var aggregate = await _repository.LoadAsync(request.Id);

            if (aggregate is null)
            {
                var state = UsState.Tennessee;
                if (request.Location_StateName == "Georgia")
                {
                    state = UsState.Georgia;
                }

                var location = new FieldLocation(
                    state,
                    new NonEmptyString(request.Location_City),
                    new NonEmptyString(request.Location_Street),
                    new NonEmptyString(request.Location_Number),
                    new NonEmptyString(request.Location_ZipCode));

                await _repository.AddAsync(new SoccerField(
                    new EntityIdentity(request.Id), location,
                    new NonEmptyString(request.Name)));
            }
            else
            {
                aggregate.UpdateName(request.Name);
            }

            await _repository.CommitChangesAsync();
        }
    }
}