using TripPlannerAPI.DTOs.AccountDTOs;

namespace TripPlannerAPI.Controllers
{
    public class UserListContainerDto
    {
        public List<UserDto> Users { get; set; }
        public UserListContainerDto()
        {
            Users = new();
        }
    }
}
