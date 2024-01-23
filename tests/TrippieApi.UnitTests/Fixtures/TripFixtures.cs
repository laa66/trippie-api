using Entities;

namespace TrippieApi.UnitTests.Fixtures;

public static class TripFixtures
{
    public static Trip GetTrip() => new()
    {
        TripId = 1
    };

    public static List<Trip> GetAllTrips() => [
        new Trip
        {
            TripId = 1,
            Id = "userId1",
            TripPoints = []
        },
        new Trip
        {
            TripId = 2,
            Id = "userId2",
            TripPoints = []
        },
        new Trip
        {
            TripId = 3,
            Id = "userId1",
            TripPoints = []
        }
    ];
}