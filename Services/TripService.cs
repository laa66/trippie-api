using Entities;

namespace Services;

public class TripService(TrippieContext trippieContext) : ITripService
{
    private readonly TrippieContext _trippieContext = trippieContext;

    public bool Delete(Trip trip)
    {
        _trippieContext.Trips.Remove(trip);
        var result = _trippieContext.SaveChanges();
        return result == 1;
    }

    public Trip Find(int id)
    {
        var result = _trippieContext.Trips.Find(id);
        return result ?? throw new Exception("Trip not found in database");
    }

    public IEnumerable<Trip> FindAll()
    {
        var result = _trippieContext.Trips.ToList();
        return result;
    }

    public User Save(User user, Trip trip)
    {
        user.UserTrips.Add(trip);
        var result = _trippieContext.Users.Update(user);
        _trippieContext.SaveChanges();
        return result.Entity;
    }
}