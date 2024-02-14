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
using TripPlannerAPI.DTOs.AccountDTOs;
using TripPlannerAPI.Repositories;

namespace TripPlannerAPI.Tests.Unit.Controller
{
    public class AccountControllerTests
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRatingRepository _userRatingRepository; //Only exists for: EnsureCreated_AdminAt101() in AccountController
        public AccountControllerTests()
        {
            _userManager = A.Fake<UserManager<User>>();
            _tokenService = A.Fake<ITokenService>();
            _userRatingRepository = A.Fake<IUserRatingRepository>(); //Only exists for: EnsureCreated_AdminAt101() in AccountController
        }


        //https://fakeiteasy.github.io/docs/5.0.0/argument-constraints/
        [Fact]
        public void AccountController_Register_ReturnsToken()
        {
            ///Arrange
            User nullUser = null;
            RegisterDto registerDto = A.Fake<RegisterDto>();
            string password = "String@123";
            registerDto.Password = password;
            IdentityResult idRes = IdentityResult.Success;
            //User user = new User { UserName = registerDto.UserName, Email = registerDto.Email };
            String token = "TestToken";
            A.CallTo(() => _userManager.FindByNameAsync(registerDto.UserName)).Returns(nullUser);

            A.CallTo(() => _userManager.CreateAsync(A<User>.That.Matches(u => u.UserName == registerDto.UserName), registerDto.Password)).Returns(idRes);             //A<User>.Ignored
            A.CallTo(() => _tokenService.GenerateToken(A<User>.That.Matches(u => u.UserName == registerDto.UserName))).Returns(token);

            var controller = new AccountController(_userManager, _tokenService, _userRatingRepository);


            ///Act
            var result = controller.Register(registerDto);


            ///Assert
            Assert.NotNull(result);
            Assert.Equal(token, result.Result.Value.Token);
        }

        [Fact]
        public void AccountController_Login_ReturnsToken()
        {
            ///Arrange:
            LoginDto loginDto = A.Fake<LoginDto>();
            loginDto.UserName = "test";
            loginDto.Password = "password";
            //User user = A.Fake<User>();
            //user.UserName = loginDto.UserName;
            User user = new User { UserName = loginDto.UserName };
            String token = "TestToken";
            A.CallTo(() => _userManager.FindByNameAsync(loginDto.UserName)).Returns(user);
            A.CallTo(() => _userManager.CheckPasswordAsync(user, loginDto.Password)).Returns(true);
            A.CallTo(() => _tokenService.GenerateToken(A<User>.That.Matches(u => u.UserName == loginDto.UserName))).Returns(token);
            var controller = new AccountController(_userManager, _tokenService, _userRatingRepository); // _userRatingRepository exists only for: EnsureCreated_AdminAt101() in AccountController

            ///Act:
            var result = controller.Login(loginDto);
            ///Assert:
            Assert.NotNull(result);
            Assert.Equal(token, result.Result.Value.Token);
        }

        [Fact]
        public void AccountController_GetCurrentUserForAuthorizedUser_ReturnsUser()
        {
            ///Arrange:
            var controller = new AccountController(_userManager, _tokenService, _userRatingRepository); // _userRatingRepository exists only for: EnsureCreated_AdminAt101() in AccountController
            String username = "test";
            String email = "test@test.test";
            User user = AuthorizeContext(controller, username);
            user.Email = email;

            //controller.ControllerContext = new ControllerContext();
            //controller.ControllerContext.HttpContext = new DefaultHttpContext { User = fakeClaimsPrincipal };

            //A.CallTo(() => fakeControllerContext.HttpContext).Returns(fakeHttpContext);
            //A.CallTo(() => fakeHttpContext.User).Returns(fakePrincipal);
            //A.CallTo(() => fakePrincipal.Identity).Returns(fakeIdentity);

            A.CallTo(() => _userManager.FindByNameAsync(username)).Returns(user);

            ///Act:
            var result = controller.GetCurrentUser();
            ///Assert:
            Assert.NotNull(result);
            Assert.Equal(email, result.Result.Value.Email);
            Assert.Equal(username, result.Result.Value.userName);
        }

        private User AuthorizeContext(AccountController controller, string username)
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