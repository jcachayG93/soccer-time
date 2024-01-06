namespace Domain.Aggregates;

public class Field
{
    public Guid Id { get; private set; }

    
    // Will use Ef Core as ORM, it requies a parameterless consturctor but can be private like here.
    private Field()
    {
        
    }
}