namespace Domain.Aggregates;

public interface ISoccerFieldAggregateRoot
{
    Guid Id { get; }
    void UpdateName(string name);
}