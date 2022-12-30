using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlannerAPI.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using TripPlannerAPI.Services;
using TripPlannerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TripPlannerAPI.Repositories;
using static TripPlannerAPI.Controllers.PostController;
using System.Net;
using TripPlannerAPI.DTOs.PostDTOs;

namespace TripPlannerAPI.Tests.Controller
{
    public class PostControllerTests
    {
        private readonly UserManager<User> _userManager;
        private readonly ITripRepository _tripRepository;
        private readonly IPostRepository _postRepository;
        public PostControllerTests()
        {
            _userManager = A.Fake<UserManager<User>>();
            _tripRepository = A.Fake<ITripRepository>();
            _postRepository = A.Fake<IPostRepository>();
        }


        //https://fakeiteasy.github.io/docs/5.0.0/argument-constraints/
        [Fact]
        public void PostController_CreatePost_ReturnsCreated()
        {
            ///Arrange
            String postContent = "TEST CONTENT!!!";
            String username = "TestUsername";
            var controller = new PostController(_userManager, _tripRepository, _postRepository);
            User user = AuthorizeContext(controller, username);

            var fakeTrip = A.Fake<Trip>();
            fakeTrip.tripId = 1;
            fakeTrip.members = new List<User>();
            fakeTrip.members.Add(user);
            fakeTrip.creator = user;
            A.CallTo(()=>_tripRepository.GetTripAsync(fakeTrip.tripId)).Returns(fakeTrip);
            PostInputDto postInput = A.Fake<PostInputDto>();
            postInput.content = postContent;
            Post post = A.Fake<Post>();
            post.Creator = user;
            post.postId = 1;
            post.Content = postContent;
            post.RelatedTrip = fakeTrip;
            A.CallTo(() => _postRepository.CreatePostAsync(A<Post>.That.Matches(x => x.Content == postContent))).Returns(post);
            ///Act
            var result = controller.CreatePost(fakeTrip.tripId, postInput);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, objRes.StatusCode);


        }

        [Fact]
        public void PostController_GetAllPosts_ReturnsOK()
        {
            ///Arrange
            var controller = new PostController(_userManager, _tripRepository, _postRepository);
            User fakeUser = AuthorizeContext(controller, "TestUserName");

            Trip fakeTrip = A.Fake<Trip>();
            fakeTrip.tripId = 1;
            fakeTrip.members = new List<User>();
            fakeTrip.creator = fakeUser;
            A.CallTo(()=>_tripRepository.GetTripAsync(fakeTrip.tripId)).Returns(fakeTrip);

            Post fakePost = A.Fake<Post>();
            fakePost.Creator = fakeUser;
            fakePost.RelatedTrip = fakeTrip;
            var fakePostDtos = new List<PostDTO>();
            fakePostDtos.Add(new PostDTO(fakePost));
            A.CallTo(() => _postRepository.GetPostsAsync(fakeTrip.tripId)).Returns(fakePostDtos);

            ///Act:
            var result = controller.GetAllPosts(fakeTrip.tripId);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objRes.StatusCode);
            Assert.True(((postsListWrapperDto)objRes.Value).posts.Count == 1);
        }


        private User AuthorizeContext(PostController controller, string username)
        {
            var fakeClaimsPrincipal = A.Fake<ClaimsPrincipal>();
            var fakeClaimsId = A.Fake<ClaimsIdentity>();
            A.CallTo(() => fakeClaimsId.Name).Returns(username);
            A.CallTo(() => fakeClaimsPrincipal.Identity).Returns(fakeClaimsId);

            controller.ControllerContext = A.Fake<ControllerContext>();
            controller.ControllerContext.HttpContext = A.Fake<HttpContext>();
            A.CallTo(() => controller.ControllerContext.HttpContext.User).Returns(fakeClaimsPrincipal);

            User user = A.Fake<User>();
            user.UserName = username;
            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(user);

            return user;
        }
    }
}
