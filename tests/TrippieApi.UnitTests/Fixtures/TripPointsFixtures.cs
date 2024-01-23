using Models;

namespace TrippieApi.UnitTests.Fixtures;

public static class TripPointsFixtures
{
    public static List<TripPoint> GetTestTripPoints() => [
        new TripPoint(49.5, 30.5, "fourth", "fourth"),
        new TripPoint(10.2, 12.9, "second", "second"),
        new TripPoint(4.2, 2.0, "first", "first"),
        new TripPoint(24.0, 22.2, "third", "third")
    ];
}