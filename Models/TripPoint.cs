using System.Text.Json.Serialization;

namespace Models;

public class TripPoint
{
    public TripPoint()
    {

    }

    public TripPoint(double longitude, double latitude, string name, string fullAddress)
    {
        Coordinates = [longitude, latitude];
        Name = name;
        FullAddress = fullAddress;

    }
    public double[] Coordinates { get; }

    public string Name { get; } 

    [JsonPropertyName("full_address")]
    public string FullAddress { get; }

    public override string ToString()
    {
        return "Coordinates: " + Coordinates[0] + ". " + Coordinates[1] + ", Name: " + Name + ", FullAddress: " + FullAddress;
    }
}