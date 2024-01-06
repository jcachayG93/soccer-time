using Domain.Aggregates;
using Domain.ValueObjects;

namespace Application.UnitTests.TestCommon.Factories;

public static class SoccerFieldFactory
{
    public static SoccerField Create()
    {
        return new SoccerField(
            EntityIdentity.Random, new FieldLocation(
                UsState.Tennessee, new NonEmptyString("Chattanooga"), new NonEmptyString("Main St"),
                new NonEmptyString("2223"), new NonEmptyString("30420")),
            new NonEmptyString("La Bombonerita"));
    }
}