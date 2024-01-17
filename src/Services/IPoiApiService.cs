using Models;

namespace Services;

public interface IPoiApiService 
{
    Task<IEnumerable<TripPoint>> GetPoiCollection(string category, double longitude, double latitude);
}