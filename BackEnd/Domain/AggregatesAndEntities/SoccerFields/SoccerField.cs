﻿using Domain.ValueObjects;

namespace Domain.Aggregates;

public class SoccerField : ISoccerFieldAggregateRoot
{
    public Guid Id { get; private set; }

    public FieldLocation Location { get; private set; }

    public string Name { get; private set; }

    public void UpdateName(string name)
    {
        Name = name;
    }

    // Will use Ef Core as ORM, it requies a parameterless consturctor but can be private like here.
    private SoccerField()
    {
        
    }

    public SoccerField(
        EntityIdentity id,
        FieldLocation location,
        NonEmptyString name)
    {
        Id = id.Value;
        Location = location;
        Name = name.Value;
    }
}
