using Entities;

namespace Services;

public interface ITripService
{
    User Save(User user, Trip trip);
    bool Delete(int id);

    Trip Find(int id);
    IEnumerable<Trip> FindUserTrips(string id);
    IEnumerable<Trip> FindAll();
}