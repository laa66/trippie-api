using KdTree;
using KdTree.Math;
using Models;

namespace Services;

public class TripGenerationService(IPoiApiService poiApiService) : ITripGenerationService
{
    private readonly IPoiApiService _poiApiService = poiApiService;

    public async Task<IEnumerable<TripPoint>> CreateTrip(int size, double longitude, double latitude, string[] categories)
    {
        List<TripPoint> tripPoints = [];

        foreach (var category in categories)
        {
            var trips = await GetSingleCategoryTrip(longitude, latitude, category);
            tripPoints.AddRange(trips);
        }

        return await OptimizeTrip(longitude, latitude, tripPoints, size);
    }

    public async Task<IEnumerable<TripPoint>> GetSingleCategoryTrip(double longitude, double latitude, string category)
    {
        return await _poiApiService.GetPoiCollection(category, longitude, latitude);
    }

    public Task<IEnumerable<TripPoint>> OptimizeTrip(double startLongitude, double startLatitude, IEnumerable<TripPoint> tripPoints, int size)
    {
        var optimizedTrip = new List<TripPoint>();


        var tree = new KdTree<double, TripPoint>(2, new DoubleMath());
        foreach (var point in tripPoints) tree.Add(point.Coordinates, point);

        for (int i=0;i<size;i++)
        {
            KdTreeNode<double, TripPoint>[] node;
            do {
                node = tree.GetNearestNeighbours([startLongitude, startLatitude], 1);
            } while(optimizedTrip.Exists(p => p.Coordinates[0] == node[0].Point[0] && p.Coordinates[1] == node[0].Point[1]));
            
            tree.RemoveAt(node[0].Point);
            optimizedTrip.Add(node[0].Value);
            startLongitude = node[0].Point[0];
            startLatitude = node[0].Point[1];
        }

        return Task.FromResult<IEnumerable<TripPoint>>(optimizedTrip);
    }

}