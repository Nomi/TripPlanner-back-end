using TripPlannerAPI.DTOs.RatingDtos;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public interface IUserRatingRepository
    {
        public Task<float> AddUserRating(User user, RatingDto rating);
    }
}
