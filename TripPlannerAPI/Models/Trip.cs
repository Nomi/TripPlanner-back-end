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

        public DateTime creationDateTime { get; set; }
        public string? type { get; set; }

        [NotMapped]
        public string startTime
        {
            get
            {
                if (date == DateTime.MinValue) //For normal DateTimes, if you don't initialize them at all then they will match DateTime.MinValue, because it is a value type rather than a reference type.
                    return null;
                return date.TimeOfDay.Hours.ToString() + ":"+date.TimeOfDay.Minutes.ToString();
            }
        }
        public float totalTime { get; set; }
        public String description { get; set; }
        public float distance { get; set; }
        public bool isRecommended { get; set; } = false;
        public List<Preference> preferences { get; set; }
        public List<Location> waypoints { get; set; }
        public List<User> members { get; set; }
        public User creator { get; set; }
        [JsonIgnore] // NEEDED to stop circular references. //Also, I have no use for this here.
        public List<User> FavoritedBy { get; set; }
        public List<Pin> Pins { get; set; }
    }
}
