namespace Entities;

public class TripPoint
{
    public TripPoint()
    {

    }

    public TripPoint(int tripPointId, double longitude, double latitude, string name, string fullAddress)
    {
        TripPointId = tripPointId;
        Longitude = longitude;
        Latitude = latitude;
        Name = name;
        FullAddress = fullAddress;
    }

    public int TripPointId { get; set; }
    public int TripId { get; set; }

    public Trip? Trip { get; set; }

    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string? Name { get; set; }
    public string? FullAddress { get; set; }

    public override string ToString()
    {
        return TripPointId + " " + TripId + " " + Longitude + " " + Latitude + " " + Name + " " + FullAddress;
    }
}