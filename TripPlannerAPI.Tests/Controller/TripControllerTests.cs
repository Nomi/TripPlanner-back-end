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
using TripPlannerAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TripPlannerAPI.Repositories;
using static TripPlannerAPI.Controllers.PostController;
using System.Net;
using static TripPlannerAPI.Controllers.TripController;

namespace TripPlannerAPI.Tests.Controller
{
    public class TripControllerTests
    {
        private readonly UserManager<User> _userManager;
        private readonly ITripRepository _tripRepository;
        public TripControllerTests()
        {
            _userManager = A.Fake<UserManager<User>>();
            _tripRepository = A.Fake<ITripRepository>();
        }


        //https://fakeiteasy.github.io/docs/5.0.0/argument-constraints/
        [Fact]
        public void TripController_CreatePost_ReturnsCreated()
        {
            ///Arrange
            String username = "TestUsername";
            var controller = new TripController(_userManager, _tripRepository);
            User user = AuthorizeContext(controller, username);

            var fakeTripInput = A.Fake<TripInputDto>();
            //fakeTripInput.date= DateTime.Now;
            //fakeTripInput.distance = 20;
            fakeTripInput.preferences = new List<string>() { "History", "Entertainment", "Sightseeing" };
            //fakeTripInput.description = "SAMPLE DESCRIPTION";
            //fakeTripInput.totalTime = 20.6F;
            fakeTripInput.startTime = "23:59";


            var fakeTrip = A.Fake<Trip>();
            //fakeTrip.tripId = 1;
            //fakeTrip.members = new List<User>();
            //fakeTrip.members.Add(user);
            //fakeTrip.creator = user;
            //fakeTrip.preferences= new List<Preference>();
            //for(int i=0;i<fakeTripInput.preferences.Count();i++)
            //{
            //    var pref = new Preference();
            //    pref.Id = i;
            //    pref.preferenceStr= fakeTripInput.preferences[i];
            //    fakeTrip.preferences.Add(pref);
            //}
            A.CallTo(() => _tripRepository.CreateTripAsync(A<Trip>.Ignored)).Returns(fakeTrip);
            
            ///Act
            var result = controller.CreateTrip(fakeTripInput);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, objRes.StatusCode);


        }

        [Fact]
        public void PostController_GetAllTripsNotCreatorOrMemberOf_ReturnsOK()
        {
            ///Arrange
            String username = "TestUsername";
            var controller = new TripController(_userManager, _tripRepository);
            User user = AuthorizeContext(controller, username);


            A.CallTo(() => _tripRepository.GetAllTripsCurrentOrFutureUserNotMemberOrCreatorAsync(user)).Returns(new List<Trip>());
            A.CallTo(()=> _tripRepository.GetFavoriteTrips(user)).Returns(new List<Trip>());
            ///Act:
            var result = controller.GetAllTripsNotCreatorOrMemberOf();
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objRes.StatusCode);
            Assert.Equal(0,((TripListContainer)objRes.Value).trips.Count);
        }

        [Fact]
        public void PostController_GetTripsByQueryParam_ReturnsOK()
        {
            ///Arrange
            String queryParam = "created-future";
            List<string> args = queryParam.Split('-').ToList();
            String username = "TestUsername";
            var controller = new TripController(_userManager, _tripRepository);
            User user = AuthorizeContext(controller, username);


            A.CallTo(() => _tripRepository.GetTripsQueryParamFilteredAsync(args[0], args[1],user)).Returns(new List<Trip>());
            A.CallTo(() => _tripRepository.GetFavoriteTrips(user)).Returns(new List<Trip>());

            ///Act:
            var result = controller.GetTripsByQueryParam(queryParam);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objRes.StatusCode);
            Assert.Equal(0, ((TripListContainer)objRes.Value).trips.Count);
        }

        [Fact]
        public void PostController_GetFavoriteTrips_ReturnsOK()
        {
            ///Arrange
            String username = "TestUsername";
            var controller = new TripController(_userManager, _tripRepository);
            User user = AuthorizeContext(controller, username);


            A.CallTo(() => _tripRepository.GetFavoriteTrips(user)).Returns(new List<Trip>());

            ///Act:
            var result = controller.GetFavoriteTrips();
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, objRes.StatusCode);
            Assert.Equal(0, ((TripListContainer)objRes.Value).trips.Count);
        }

        [Fact]
        public void PostController_GetTrip_ForTripThatDoesntExistReturnsNotFound()
        {
            ///Arrange
            var controller = new TripController(_userManager,_tripRepository);
            int tripId = 1;
            String username = "testusername";
            User user = AuthorizeContext(controller, username);

            Trip nullTrip = null;
            A.CallTo(() => _tripRepository.GetTripAsync(tripId)).Returns(nullTrip);

            ///Act:
            var result = controller.GetTrip(tripId);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, objRes.StatusCode);
        }

        [Fact]
        public void PostController_JoinTrip_ForTripThatDoesntExistReturnsNotFound()
        {
            ///Arrange
            var controller = new TripController(_userManager, _tripRepository);
            int tripId = 1;
            String username = "testusername";
            User user = AuthorizeContext(controller, username);

            Trip nullTrip = null;
            A.CallTo(() => _tripRepository.GetTripAsync(tripId)).Returns(nullTrip);

            ///Act:
            var result = controller.JoinTrip(tripId);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, objRes.StatusCode);
        }
        [Fact]
        public void PostController_AddFavoriteTrip_ForTripThatDoesntExistReturnsNotFound()
        {
            ///Arrange
            var controller = new TripController(_userManager, _tripRepository);
            int tripId = 1;
            String username = "testusername";
            User user = AuthorizeContext(controller, username);

            A.CallTo(() => _tripRepository.GetFavoriteTrips(user)).Returns(new List<Trip>());
            Trip nullTrip = null;
            A.CallTo(() => _tripRepository.GetTripAsync(tripId)).Returns(nullTrip);

            ///Act:
            var result = controller.GetTrip(tripId);
            ObjectResult objRes = (ObjectResult)result.Result.Result;

            ///Assert:
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.NotFound, objRes.StatusCode);
        }
        private User AuthorizeContext(TripController controller, string username, User useThisForDataForNewUser = null)
        {
            var fakeClaimsPrincipal = A.Fake<ClaimsPrincipal>();
            var fakeClaimsId = A.Fake<ClaimsIdentity>();
            A.CallTo(() => fakeClaimsId.Name).Returns(username);
            A.CallTo(() => fakeClaimsPrincipal.Identity).Returns(fakeClaimsId);

            controller.ControllerContext = A.Fake<ControllerContext>();
            controller.ControllerContext.HttpContext = A.Fake<HttpContext>();
            A.CallTo(() => controller.ControllerContext.HttpContext.User).Returns(fakeClaimsPrincipal);

            User user = A.Fake<User>();
            if (useThisForDataForNewUser != null)
                user = useThisForDataForNewUser;
            user.UserName = username;
            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(user);

            return user;
        }
    }
}
