namespace Application.Slices.SoccerFields.Queries.GetList;

public class SoccerFieldsListViewModel
{
    public required IEnumerable<SoccerFieldsListLookup> Lookups
    {
        get;
        init;
    }
    
    public class SoccerFieldsListLookup
    {
        public required Guid Id { get; init; }
    
        public required string Name { get; init; }
    }
}

