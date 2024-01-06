using System.Security.Cryptography;

namespace Domain.ValueObjects;

/// <summary>
/// A state in the USA
/// </summary>
public record UsState
{
    /*
     * Abstraction. All details on how to calculate the states is hidden,
     * and is irrelevant. This Value object provides a very explicit interface which
     * "Abstracts" those details.
     */
    private static IEnumerable<Tuple<string, string>> _data
        = StateListFactory.Create();

    public string Name { get; }

    public string Abbreviation { get; }
    
    /*
     * Using a private constructor, makes it impossible for a user to
     * try to create a state that does not exist.
     */
    private UsState(
        string abbreviation)
    {
        var match = UsState._data
            .First(x =>
                x.Item1 == abbreviation);

        Abbreviation = abbreviation;
        Name = match.Item2;
    }

    public static UsState Tennessee =>
        new("TN");

    public static UsState Georgia =>
        new("GA");

    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }

    static class StateListFactory
{
    public static IEnumerable<Tuple<string, string>> Create()
    {
        /*
         * For a real project, I would make this better. But this is an example
         * from stack overflow, addapted to this project.
         * https://stackoverflow.com/questions/7367529/standardized-us-states-array-and-countries-array
         */
            var states = new List<US_State>(50);
    states.Add(new US_State("AL", "Alabama"));
    states.Add(new US_State("AK", "Alaska"));
    states.Add(new US_State("AZ", "Arizona"));
    states.Add(new US_State("AR", "Arkansas"));
    states.Add(new US_State("CA", "California"));
    states.Add(new US_State("CO", "Colorado"));
    states.Add(new US_State("CT", "Connecticut"));
    states.Add(new US_State("DE", "Delaware"));
    states.Add(new US_State("DC", "District Of Columbia"));
    states.Add(new US_State("FL", "Florida"));
    states.Add(new US_State("GA", "Georgia"));
    states.Add(new US_State("HI", "Hawaii"));
    states.Add(new US_State("ID", "Idaho"));
    states.Add(new US_State("IL", "Illinois"));
    states.Add(new US_State("IN", "Indiana"));
    states.Add(new US_State("IA", "Iowa"));
    states.Add(new US_State("KS", "Kansas"));
    states.Add(new US_State("KY", "Kentucky"));
    states.Add(new US_State("LA", "Louisiana"));
    states.Add(new US_State("ME", "Maine"));
    states.Add(new US_State("MD", "Maryland"));
    states.Add(new US_State("MA", "Massachusetts"));
    states.Add(new US_State("MI", "Michigan"));
    states.Add(new US_State("MN", "Minnesota"));
    states.Add(new US_State("MS", "Mississippi"));
    states.Add(new US_State("MO", "Missouri"));
    states.Add(new US_State("MT", "Montana"));
    states.Add(new US_State("NE", "Nebraska"));
    states.Add(new US_State("NV", "Nevada"));
    states.Add(new US_State("NH", "New Hampshire"));
    states.Add(new US_State("NJ", "New Jersey"));
    states.Add(new US_State("NM", "New Mexico"));
    states.Add(new US_State("NY", "New York"));
    states.Add(new US_State("NC", "North Carolina"));
    states.Add(new US_State("ND", "North Dakota"));
    states.Add(new US_State("OH", "Ohio"));
    states.Add(new US_State("OK", "Oklahoma"));
    states.Add(new US_State("OR", "Oregon"));
    states.Add(new US_State("PA", "Pennsylvania"));
    states.Add(new US_State("RI", "Rhode Island"));
    states.Add(new US_State("SC", "South Carolina"));
    states.Add(new US_State("SD", "South Dakota"));
    states.Add(new US_State("TN", "Tennessee"));
    states.Add(new US_State("TX", "Texas"));
    states.Add(new US_State("UT", "Utah"));
    states.Add(new US_State("VT", "Vermont"));
    states.Add(new US_State("VA", "Virginia"));
    states.Add(new US_State("WA", "Washington"));
    states.Add(new US_State("WV", "West Virginia"));
    states.Add(new US_State("WI", "Wisconsin"));
    states.Add(new US_State("WY", "Wyoming"));

    return states.Select(x=>
        new Tuple<string, string>(x.Abbreviation, x.Name))
        .ToArray();
    }

    // Using the class here for practical reasons. Nested and private, it is
    // only accessible by the containing class.
    private class US_State
    {
        public string Abbreviation { get; }
        public string Name { get; }

        public US_State(
            string abbreviation, string name)
        {
            Abbreviation = abbreviation;
            Name = name;
        }
    }
}
}
