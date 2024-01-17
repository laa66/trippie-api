using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class TripService(TrippieContext trippieContext) : ITripService
{
    private readonly TrippieContext _trippieContext = trippieContext;

    public bool Delete(int id)
    {
        var trip = Find(id);
        _trippieContext.Trips.Remove(trip);
        var result = _trippieContext.SaveChanges();
        return result == 1;
    }

    public Trip Find(int id)
    {
        var result = _trippieContext.Trips.Find(id);
        return result ?? throw new TripNotFoundException("Trip not found in repository");
    }

    public IEnumerable<Trip> FindAll()
    {
        return _trippieContext.Trips
            .Include(t => t.TripPoints)
            .ToList();
    }

    public IEnumerable<Trip> FindUserTrips(string id)
    {
        return _trippieContext.Trips
            .Where(u => u.Id == id)
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