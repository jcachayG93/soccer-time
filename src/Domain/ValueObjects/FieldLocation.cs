namespace Domain.ValueObjects;

public record FieldLocation
{
    public string StateName { get; }
    public string City { get;}

    public string Street { get; }

    public string Number { get; }

    public string ZipCode { get; }

    /*
     * TODO: We forgot the state, but the state should be a Value object itself,
     * not just a random string. 
     */
    public FieldLocation(
        UsState state,
        NonEmptyString city,
        NonEmptyString street,
        NonEmptyString number,
        NonEmptyString zipCode
        )
    {
        StateName = state.Name;
        City = city.Value;
        Street = street.Value;
        Number = number.Value;
        ZipCode = zipCode.Value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Number} {Street} {City}, {StateName}. {ZipCode}";
    }
}