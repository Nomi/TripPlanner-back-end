using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TripPlannerAPI.Data;
using TripPlannerAPI.DTOs.RatingDtos;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public class UserRatingRepository : IUserRatingRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRatingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<float> AddUserRating(User user, RatingDto rating)
        {
            await _appDbContext.Ratings.AddAsync(new Rating
            {
                IsOrganizer = rating.IsOrganizer,
                RatingPoints = rating.RatingPoints,
                User = user
            });
            
            await _appDbContext.SaveChangesAsync();
           
            var count = await _appDbContext.Ratings
            .Where(r => r.User == user)
            .Where(rating => rating.IsOrganizer == rating.IsOrganizer)
            .CountAsync();

            var points = await _appDbContext.Ratings
            .Where(r => r.User == user)
            .Where(rating => rating.IsOrganizer == rating.IsOrganizer)
            .SumAsync(r => r.RatingPoints);

            return points/ count;
        }
    }
}
