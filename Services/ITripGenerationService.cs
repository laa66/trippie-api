using Models;

namespace Services;

public interface ITripGenerationService {

    IEnumerable<TripPoint> CreateTrip(int size, 
    double longitude, 
    double latitude, 
    string[] categories);

    IEnumerable<TripPoint> OptimizeTrip(IEnumerable<TripPoint> tripPoints);

    IEnumerable<TripPoint> GetSingleCategoryTrip(double longitude,
    double latitude,
    string category);
}