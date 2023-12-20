using System.Text.Json.Serialization;

namespace Models;

public class TripPoint(double longitude, double latitude, string name, string fullAddress)
{
    public double Longitude { get; } = longitude;
    
    public double Latitude { get; } = latitude; 

    public string Name { get; } = name;

    [JsonPropertyName("full_address")]
    public string FullAddress { get; } = fullAddress;
}