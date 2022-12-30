using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripPlannerAPI.DTOs.PostDTOs;
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
        private readonly ITripRepository _tripRepository;
        private readonly IPostRepository _postRepository;
        public PostController(UserManager<User> userManager, ITripRepository tripRepository, IPostRepository postRepository)
        {
            _userManager = userManager;
            this._tripRepository = tripRepository;
            this._postRepository = postRepository;
        }
        [Authorize]
        [HttpPost("trip/{tripId}")]
        [ProducesResponseType(typeof(MsgOnlyResp), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(MsgOnlyResp), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(MsgOnlyResp), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MsgOnlyResp>> CreatePost(int tripId, PostInputDto _post)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = await _tripRepository.GetTripAsync(tripId);
            if(trip==null)
            {
                MsgOnlyResp wrongTripIdMsgBody = new();
                wrongTripIdMsgBody.message = "No trip corresponding to provided tripId could be found.";
                return StatusCode((int)HttpStatusCode.BadRequest, wrongTripIdMsgBody);
            }
            if (trip.creator.Id != user.Id && !trip.members.Any(u => u.Id == user.Id))
            {
                MsgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "You need to be a member or a creator of the trip to create related posts.";
                return StatusCode((int)HttpStatusCode.Forbidden, errorMsgBody);
            }
            Post post = new Post();
            post.Creator = user;
            post.RelatedTrip = trip;
            post.Content = _post.content;
            post.CreationDateTime = DateTime.Now;
            var result = await _postRepository.CreatePostAsync(post);

            var respBody = new MsgOnlyResp();
            respBody.message = "Success";
            return StatusCode((int)HttpStatusCode.Created, respBody);
        }
        [Authorize]
        [HttpGet("trip/{tripId}")]
        [ProducesResponseType(typeof(postsListWrapperDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MsgOnlyResp), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(MsgOnlyResp), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<postsListWrapperDto>> GetAllPosts(int tripId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var trip = await _tripRepository.GetTripAsync(tripId);
            if (trip == null)
            {
                MsgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "No trip corresponding to provided tripId could be found.";
                return StatusCode((int)HttpStatusCode.BadRequest, errorMsgBody);
            }
            if (trip.creator.Id != user.Id && !trip.members.Any(u => u.Id == user.Id))
            {
                MsgOnlyResp errorMsgBody = new();
                errorMsgBody.message = "You need to be a member or a creator of the trip to see related posts.";
                return StatusCode((int)HttpStatusCode.Forbidden, errorMsgBody);
            }
            var result = await _postRepository.GetPostsAsync(tripId);
            var respBody = new postsListWrapperDto();
            respBody.posts = (List<PostDTO>)result;
            return StatusCode((int)HttpStatusCode.OK, respBody);
        }
    }
}
