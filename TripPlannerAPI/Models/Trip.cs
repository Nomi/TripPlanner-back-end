using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace TripPlannerAPI.Models
{
    public class TripContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
    }
    public class Trip
    {
        [Key]
        public int tripId { get; set; }
        public DateOnly date { get; set; }
        public string? type { get; set; }
        public List<string> preferences { get; set; }
        public List<Location> waypoints { get; set; }
        public List<User> members { get; set; }
        public User creator { get; set; }
    }
}
