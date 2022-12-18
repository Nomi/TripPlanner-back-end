using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TripPlannerAPI.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]//NEEDED to stop circular references. //I really wanted this one to not be JsonIgnored
        [InverseProperty("FavoritedBy")]
        public List<Trip> FavoriteTrips { get; set; }

        [InverseProperty("creator")]
        [JsonIgnore]//NEEDED to stop circular references.
        public List<Trip> CreatedTrips { get; set; }

        [InverseProperty("members")]
        [JsonIgnore]//NEEDED to stop circular references.
        public List<Trip> TripsJoined { get; set; }

        public User()
        {
            FavoriteTrips=new List<Trip>(); 
            CreatedTrips=new List<Trip>();
            TripsJoined=new List<Trip>();
        }
    }
}
