using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripPlannerAPI.DTOs.TripTypePreferenceDTOs;
using TripPlannerAPI.Models;
using TripPlannerAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripTypePreferenceController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITripTypePreferenceRepository _tripTypePreferenceRepository;

        public TripTypePreferenceController(UserManager<User> userManager, ITripTypePreferenceRepository tripTypePreferenceRepository)
        {
            _userManager = userManager;
            _tripTypePreferenceRepository = tripTypePreferenceRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<RequestTripTypeDto>>> Get()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (user == null)
                return NotFound("User doesnt exsit");

            var preferences = await _tripTypePreferenceRepository.GetPreferences(user);

            if( preferences == null || preferences.Count == 0 )
            {
                return NoContent();
            }

            return Ok(preferences);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update(RequestTripTypeDto request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
                return NotFound();

            await _tripTypePreferenceRepository.UpdatePreferences(user, request);

            return Ok("Preferences updated sucessfully");
        }
    }
}
