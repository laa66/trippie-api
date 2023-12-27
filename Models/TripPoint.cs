using System.Text.Json.Serialization;

namespace Models;

public class TripPoint(double longitude, double latitude, string name, string fullAddress)
{
    public double[] Coordinates { get; } = [longitude, latitude];

    public string Name { get; } = name;

    [JsonPropertyName("full_address")]
    public string FullAddress { get; } = fullAddress;

    public override string ToString()
    {
        return "Coordinates: " + Coordinates[0] + ". " + Coordinates[1] + ", Name: " + Name + ", FullAddress: " + FullAddress;
    }
}