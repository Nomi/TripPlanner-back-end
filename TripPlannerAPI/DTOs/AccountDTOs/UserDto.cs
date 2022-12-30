using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs.AccountDTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public float OrganizerRating { get; set; }
        public float UserRating { get; set; }

        public UserDto(User user)
        {
            Email = user.Email;
            Username = user.UserName;
            OrganizerRating = user.OrganizerRating;
            UserRating = user.UserRating;
        }

    }
}
