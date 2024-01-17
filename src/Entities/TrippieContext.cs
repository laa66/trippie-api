using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class TrippieContext(DbContextOptions<TrippieContext> dbContextOptions) : IdentityDbContext<User>(dbContextOptions)
{
    public override DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripPoint> TripPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
        .HasKey(p => p.TripId);
        
        modelBuilder.Entity<Trip>()
        .HasMany(p => p.TripPoints)
        .WithOne(t => t.Trip)
        .HasForeignKey(t => t.TripId);

        modelBuilder.Entity<User>()
        .HasMany(u => u.UserTrips)
        .WithOne(t => t.User)
        .HasForeignKey(t => t.Id);

        base.OnModelCreating(modelBuilder);
    }

}