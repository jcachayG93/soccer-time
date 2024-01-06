namespace Domain.ValueObjects;

public record FieldLocation
{
    public string City { get;}

    public string Street { get; }

    public string Number { get; }

    public string ZipCode { get; }

    public FieldLocation()
    {
        
    }
}