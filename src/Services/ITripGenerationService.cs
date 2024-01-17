using Models;

namespace Services;

public interface ITripGenerationService 
{

    Task<IEnumerable<TripPoint>> CreateTrip(int size, 
    double longitude, 
    double latitude, 
    string[] categories);

    Task<IEnumerable<TripPoint>> OptimizeTrip(double startLongitude, double startLatitude, IEnumerable<TripPoint> tripPoints, int size);

    Task<IEnumerable<TripPoint>> GetSingleCategoryTrip(double longitude,
    double latitude,
    string category);
}