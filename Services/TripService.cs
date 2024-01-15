using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class TripService(TrippieContext trippieContext) : ITripService
{
    private readonly TrippieContext _trippieContext = trippieContext;

    public bool Delete(int id)
    {
        var trip = _trippieContext.Trips.Find(id);
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
        var result = _trippieContext.Trips
            .Include(t => t.TripPoints)
            .ToList();
        return result;
    }

    public IEnumerable<Trip> FindUserTrips(string id)
    {
        return _trippieContext.Trips.Where(u => u.Id == id)
            .Include(p => p.TripPoints);
    }

    public User Save(User user, Trip trip)
    {
        user.UserTrips.Add(trip);
        var result = _trippieContext.Users.Update(user);
        _trippieContext.SaveChanges();
        return result.Entity;
    }
}