using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripPlannerAPI.DTOs.RatingDtos;
using TripPlannerAPI.Models;
using TripPlannerAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRatingRepository _userRatingRepository;

        public UserRatingController(UserManager<User> userManager, IUserRatingRepository userRatingRepository)
        {
            _userManager = userManager;
            _userRatingRepository = userRatingRepository;
        }
     
        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<RatingDto>> AddRating(RatingDto rating)
        {
            var user = await _userManager.FindByNameAsync(rating.UserName);

            if (user == null)
                return NotFound("User doesnt exist");

            var points = await _userRatingRepository.AddUserRating(user, rating);

            if(rating.IsOrganizer)
            {
                user.OrganizerRating = points;
            }
            else
                user.UserRating = points;

            await _userManager.UpdateAsync(user);

            return Ok("Rating Added sucessfully");
        }
    }
}
