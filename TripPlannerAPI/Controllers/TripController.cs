using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using TripPlannerAPI.DTOs;
using TripPlannerAPI.Models;
using TripPlannerAPI.Repositories;
using TripPlannerAPI.Services;

namespace TripPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TripRepository tripRepository;
        public TripController(UserManager<User> userManager,TripRepository tripRepository)
        {
            _userManager = userManager;
            this.tripRepository = tripRepository;
        }


        public class tripInput { public DateOnly date; public String type; public List<String> preferences; public List<Location> waypoints; }





        [Authorize]
        [HttpPost("new")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<LoginResponse>> Register(tripInput _trip)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = new Trip { date = _trip.date, creator = user, members = new List<User>(), preferences = _trip.preferences, waypoints = _trip.waypoints };
            if (trip == null)
                return ValidationProblem("Error, failed to create user. Potentially due to validation issues.");
            var result =
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return ValidationProblem("Validation failed.");
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new LoginResponse { Token = await _tokenService.GenerateToken(user) };
        }
    }
}
