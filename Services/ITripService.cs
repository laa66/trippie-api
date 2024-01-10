using Entities;

namespace Services;

public interface ITripService
{
    User Save(User user, Trip trip);
    bool Delete(Trip trip);

    Trip Find(int id);

    IEnumerable<Trip> FindAll();
}