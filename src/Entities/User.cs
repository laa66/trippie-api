using Microsoft.AspNetCore.Identity;

namespace Entities;

public class User : IdentityUser
{
    public string? UserLogin { get; set; }
    public DateTime JoinDate { get; set; }
    public ICollection<Trip> UserTrips { get; } = new List<Trip>();

}