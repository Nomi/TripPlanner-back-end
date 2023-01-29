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
        private readonly UserManager<User> _userManager; //exists only for EnsureCreated_AdminAt101

        public UserRatingRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager; //exists only for EnsureCreated_AdminAt101
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
        /// <summary>
        /// The following methods is just a stopgap solution for seeding the database with admin role if it doesn't exist.
        /// Clearly, a lot of things need to change for Security (e.g. the password and username will be visible publicly in this file, without encryption),
        /// and refactoring.
        /// </summary>
        /// <exception cref="Exception">Thrown in case of the following errors: 1.) Database doesn't exist. </exception>
        public async Task<int> EnsureCreated_AdminAt101()
        {
            string admUsrName = "Admin@101"; ///TODO: need to remove it being hardcoded.
            string admPass = "Admin@101"; ///TODO: need to remove it being hardcoded, and to make the password secret.
            string admEmail = "admin@admin.admin"; ///TODO: need to remove it being hardcoded.
            string admRoleName = "admin";
            //Check if the initialization is needed:
            var adminAt101 = _appDbContext.Users.First(u => (u.UserName == admUsrName)); 
            var adminRole = _appDbContext.Roles.First(r => (r.Name == admRoleName)); //The role should already be there (refer to AppDbContext class).
            if (adminAt101 != null && _appDbContext.UserRoles.Any(x => ((x.UserId == adminAt101.Id) && (x.RoleId == adminRole.Id)))) ///TODO: need to remove it being hardcoded.
            {
                return 1;   // DB has already been seeded
            }
            if (null == adminAt101)
            {
                adminAt101 = new User { UserName = admUsrName, Email = admEmail };

                var result = await _userManager.CreateAsync(adminAt101, admPass); 
            }
            _appDbContext.SaveChanges();
            var roleResult = await _userManager.AddToRoleAsync(adminAt101, adminRole.Name);
            return 0;
        }
    }
}
