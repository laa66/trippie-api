using Microsoft.AspNetCore.Identity;

namespace Entities;

public class User : IdentityUser
{
    public DateTime JoinDate { get; set; }

    public ICollection<Trip> UserTrips { get; } = new List<Trip>();

}