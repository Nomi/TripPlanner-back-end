using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TripPlannerAPI.Models
{
    public class Trip
    {
        [Key]
        public int tripId { get; set; }
        public DateTime date { get; set; }
        public string? type { get; set; }

        public List<Preference> preferences { get; set; }
        public List<Location> waypoints { get; set; }
        public List<User> members { get; set; }
        public User creator { get; set; }
        [JsonIgnore] // NEEDED to stop circular references. //Also, I have no use for this here.
        public List<User> FavoritedBy { get; set; }
    }
}
