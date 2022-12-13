using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripPlannerAPI.DTOs;
using TripPlannerAPI.Models;
using TripPlannerAPI.Repositories;
using static TripPlannerAPI.Controllers.TripController;

namespace TripPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITripRepository tripRepository;
        private readonly IPostRepository postRepository;
        public PostController(UserManager<User> userManager, ITripRepository tripRepository, IPostRepository postRepository)
        {
            _userManager = userManager;
            this.tripRepository = tripRepository;
            this.postRepository = postRepository;
        }


        public class postInput { public String content { get; set; } }
        [Authorize]
        [HttpPost("trip/{tripId}")]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<msgOnlyResp>> CreateTrip(int tripId, postInput _post)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = await tripRepository.GetTripAsync(tripId);
            if(trip==null)
            {
                msgOnlyResp wrongTripIdMsgBody = new();
                wrongTripIdMsgBody.message = "No trip corresponding to provided tripId could be found.";
                return StatusCode((int)HttpStatusCode.BadRequest, wrongTripIdMsgBody);
            }
            if (trip.creator.Id != user.Id && !trip.members.Any(u => u.Id == user.Id))
            {
                msgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "You need to be a member or a creator of the trip to create related posts.";
                return StatusCode((int)HttpStatusCode.Forbidden, errorMsgBody);
            }
            Post post = new Post();
            post.Creator = user;
            post.RelatedTrip = trip;
            post.Content = _post.content;
            var result = await postRepository.CreatePostAsync(post);

            var respBody = new msgOnlyResp();
            respBody.message = "Success";
            return StatusCode((int)HttpStatusCode.Created, respBody);
        }

        public class postsListWrapper { public List<PostDTO> posts { get; set; } }
        [Authorize]
        [HttpGet("trip/{tripId}")]
        [ProducesResponseType(typeof(postsListWrapper), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(msgOnlyResp), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<msgOnlyResp>> GetAllPosts(int tripId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = await tripRepository.GetTripAsync(tripId);
            if (trip == null)
            {
                msgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "No trip corresponding to provided tripId could be found.";
                return StatusCode((int)HttpStatusCode.BadRequest, errorMsgBody);
            }
            if (trip.creator.Id != user.Id && !trip.members.Any(u => u.Id == user.Id))
            {
                msgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "You need to be a member or a creator of the trip to see related posts.";
                return StatusCode((int)HttpStatusCode.Forbidden, errorMsgBody);
            }
            var result = await postRepository.GetPostsAsync(tripId);
            var respBody = new postsListWrapper();
            respBody.posts = (List<PostDTO>)result;
            return StatusCode((int)HttpStatusCode.OK, respBody);
        }
    }
}
