using Models;

namespace Services;

public class PoiApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IPoiApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly IConfiguration _configuration = configuration;
    public async Task<IEnumerable<TripPoint>> GetPoiCollection(string category, double longitude, double latitude) 
    {
        string? token = _configuration.GetValue<string>("MAP_API_PUBLIC_TOKEN");

        UriBuilder uriBuilder = new("https://api.mapbox.com/")
        {
            Path = $"/search/searchbox/v1/category/{category}",
            Query = $"access_token={token}&limit=25&poi_category_exclusions=services",
        };
        
        var response = await _httpClient.GetAsync(uriBuilder.Uri);
        
        response.EnsureSuccessStatusCode();

        PoiApiResponse? poiApiResponse = await response.Content.ReadFromJsonAsync<PoiApiResponse>();

        return poiApiResponse?.Features
            .Select(feature => new TripPoint(
                feature.Geometry.Coordinates[0],
                feature.Geometry.Coordinates[1],
                feature.Properties.Name,
                feature.Properties.FullAddress
            )) ?? Enumerable.Empty<TripPoint>();
    }
}