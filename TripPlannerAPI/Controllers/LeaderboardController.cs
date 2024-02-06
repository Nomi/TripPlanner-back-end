using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripPlannerAPI.DTOs.LeaderboardDTOs;
using TripPlannerAPI.Models;
using TripPlannerAPI.Repositories;

namespace TripPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        ILeaderboardRepository _leaderboardRepository;
        public LeaderboardController(ILeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        [Authorize]
        [HttpGet("{type}")]
        [ProducesResponseType(typeof(ListLeaderboardTravellerDTO), 200)]
        public async Task<ActionResult<ListLeaderboardTravellerDTO>> GetLeaderboard(string type)
        {
            ListLeaderboardTravellerDTO respBody = new();
            respBody.travellers = (List<LeaderboardTravellerDTO>) await _leaderboardRepository.GetLeaderboard(type);
            return Ok(respBody);
        }
    }
}
