using TripPlannerAPI.Models;

namespace TripPlannerAPI.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}