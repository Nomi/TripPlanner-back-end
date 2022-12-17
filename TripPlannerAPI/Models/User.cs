using Microsoft.AspNetCore.Identity;

namespace TripPlannerAPI.Models
{
    public class User : IdentityUser
    {
        public List<Trip> favoriteTrips = new List<Trip>();
    }
}
