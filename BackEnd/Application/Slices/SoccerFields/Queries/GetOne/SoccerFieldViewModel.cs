namespace Application.Slices.SoccerFields.Queries.GetOne;

public class SoccerFieldViewModel
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Address { get; init; }
}