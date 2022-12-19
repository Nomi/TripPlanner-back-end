using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TripPlannerAPI.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            FavoriteTrips = new List<Trip>();
            CreatedTrips = new List<Trip>();
            TripsJoined = new List<Trip>();
            OrganizerRating = 0;// Avg of all ratings as an organizer. //0 means unrated. Actual value is a float between 1 and 5 (inclusive of 1 and 5).
            UserRating = 0; //Avg of all ratings as a user.//0 means unreated. Actual value is a float between 1 and 5 (inclusive of 1 and 5).
            
            ////For frontend testing:
            //Random r = new Random();
            //OrganizerRating = (float)r.Next(0, 4) + (float)r.NextDouble();
            //UserRating = (float)r.Next(0, 4) + (float)r.NextDouble();
        }
        public float OrganizerRating { get; set; }
        public float UserRating { get; set; }
        [JsonIgnore]//NEEDED to stop circular references. //I really wanted this one to not be JsonIgnored
        [InverseProperty("FavoritedBy")]
        public List<Trip> FavoriteTrips { get; set; }

        [InverseProperty("creator")]
        [JsonIgnore]//NEEDED to stop circular references.
        public List<Trip> CreatedTrips { get; set; }

        [InverseProperty("members")]
        [JsonIgnore]//NEEDED to stop circular references.
        public List<Trip> TripsJoined { get; set; }
    }
}
