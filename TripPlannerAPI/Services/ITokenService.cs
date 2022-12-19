using TripPlannerAPI.Models;

namespace TripPlannerAPI.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}