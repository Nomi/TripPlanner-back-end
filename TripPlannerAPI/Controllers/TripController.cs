using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.CodeDom;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
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
        private readonly ITripRepository tripRepository;
        public TripController(UserManager<User> userManager, ITripRepository tripRepository)
        {
            _userManager = userManager;
            this.tripRepository = tripRepository;
        }


        public class tripInput { public DateTime date { get; set; } public String type { get; set; } public List<String> preferences { get; set; } public List<Location> waypoints { get; set; } }
        public class msgOnlyResp { public string message { get; set; } }

        [Authorize]
        [HttpPost("new")]
        [ProducesResponseType(typeof(msgOnlyResp), 201)]
        //[ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<msgOnlyResp>> CreateTrip(tripInput _trip)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = new Trip { type = _trip.type, date = _trip.date, creator = user, members = new List<User>(), waypoints = _trip.waypoints };
            trip.preferences = new List<Preference>();
            for (int i = 0; i < _trip.preferences.Count(); i++)
            {
                trip.preferences.Add(new Preference(_trip.preferences[i]));
            }
            var result = await tripRepository.CreateTripAsync(trip);

            var respBody = new msgOnlyResp();
            respBody.message = "Success";
            return StatusCode((int)HttpStatusCode.Created, respBody);
        }


        public class tripListContainer { public List<Trip> trips { get; set; } }
        [Authorize]
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<Trip>), 200)]
        public async Task<ActionResult<tripListContainer>> GetAllTripsNotCreatorOrMemberOf()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await tripRepository.GetTripsNotMemberOrCreatorAsync(user);

            var respBody = new tripListContainer();
            respBody.trips = (List<Trip>)result;
            return StatusCode((int)HttpStatusCode.OK, respBody);
        }

        [Authorize]
        [HttpGet("all/{tripId}")]
        [ProducesResponseType(typeof(Trip), 200)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Trip>> GetTrip(int tripId)
        { 
            var result = await tripRepository.GetTripAsync(tripId);
            if (result == null)
            {
                var respBody = new msgOnlyResp(); respBody.message = "Trip with given Id not found.";
                return StatusCode((int)HttpStatusCode.NotFound, respBody);
            }
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [Authorize]
        [HttpPost("all/{tripId}/join")]
        [ProducesResponseType(typeof(msgOnlyResp), 200)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Trip>> JoinTrip(int tripId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = await tripRepository.GetTripAsync(tripId);
            if (trip == null)
            {
                var respBody = new msgOnlyResp(); respBody.message = "Failure: Trip with given Id not found.";
                return StatusCode((int)HttpStatusCode.NotFound, respBody);
            }
            trip.members.Add(user);
            var result = await tripRepository.UpdateTripAsync(trip);
            var resBody = new msgOnlyResp();
            resBody.message = "Success.";
            int statusCode = (int)HttpStatusCode.OK;
            if(result==null)
            {
                resBody.message = "Failure while trying to update trip.";
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            return StatusCode(statusCode, resBody);
        }
    }
}
