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

namespace TripPlannerAPI.Tests.Controller
{
    public class AccountControllerTests
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AccountControllerTests()
        {
            _userManager = A.Fake<UserManager<User>>();
            _tokenService = A.Fake<ITokenService>();
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
            A.CallTo(()=>_userManager.FindByNameAsync(registerDto.UserName)).Returns(nullUser);

            A.CallTo(() => _userManager.CreateAsync(A<User>.That.Matches(u => u.UserName == registerDto.UserName), registerDto.Password)).Returns(idRes);             //A<User>.Ignored
            A.CallTo(() => _tokenService.GenerateToken(A<User>.That.Matches(u => u.UserName == registerDto.UserName))).Returns(token);

            var controller = new AccountController(_userManager, _tokenService);


            ///Act
            var result = controller.Register(registerDto);


            ///Assert
            Assert.NotNull(result);
            Assert.Equal(token, result.Result.Value.Token);
        }
    }
}
