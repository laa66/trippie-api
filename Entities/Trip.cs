namespace Entities;

public class Trip
{
    public int TripId { get; set; }
    public User? User { get; set; }
    public string Id { get; set; }
    public string? Title { get; set; }
    public DateTime TripDate { get; set; }
    public List<TripPoint> TripPoints { get; set; } = new List<TripPoint>();
}