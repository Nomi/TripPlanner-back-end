using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;
using TripPlannerAPI.DTOs;
using TripPlannerAPI.DTOs.AccountDTOs;
using TripPlannerAPI.Models;
using TripPlannerAPI.Services;

namespace TripPlannerAPI.Controllers
{
    public class LoginResponseDto { public string Token { get; set; } }
    public class GetUserResponseDto { public string username { get; set; } }
    public class UserMiniDto
    {
        public string Email { get; set; }
        public string userName { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private const string adminRoleString = "Admin";
        private const string userRoleString = "User";

        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized("The provided username-password pair does not match any accounts.");

            return new LoginResponseDto { Token = await _tokenService.GenerateToken(user) };
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponseDto),200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<LoginResponseDto>> Register(RegisterDto registerDto)
        {
            if (null != await _userManager.FindByNameAsync(registerDto.UserName))
                return StatusCode((int)HttpStatusCode.Conflict, "The requested username is already in use.");

            var user = new User { UserName = registerDto.UserName, Email = registerDto.Email };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                return ValidationProblem("Validation failed.");
            }

            await _userManager.AddToRoleAsync(user, userRoleString);

            return new LoginResponseDto { Token = await _tokenService.GenerateToken(user) };
        }

        [Authorize]
        [HttpGet("current_user")]
        [ProducesResponseType(typeof(UserMiniDto), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<UserMiniDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();
            return new UserMiniDto
            {
                Email = user.Email,
                userName = user.UserName
            };
        }

        [Authorize]
        [HttpGet("user/{username}")]
        [ProducesResponseType(typeof(GetUserResponseDto), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<GetUserResponseDto>> GetUser(string username)
        {
            var callingUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (callingUser == null)
                return Unauthorized("Unauthorized.");
            var user = await _userManager.FindByNameAsync(username);
            if(user==null)
                return NotFound("User not found.");
            return new GetUserResponseDto { username = username };
        }

        [Authorize]
        [HttpDelete("user/{username}")]
        [ProducesResponseType(typeof(MsgOnlyResp), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<GetUserResponseDto>> DeleteUser(string username)
        {
            if (User.Identity == null)
                return Unauthorized("Unauthorized.");
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(User.Identity.Name));
            bool isAdmin = false;
            for (int i = 0; i < roles.Count(); i++)
            {
                if (roles[i] == adminRoleString)
                    isAdmin = true;
            }
            if (isAdmin == false)
                return Unauthorized("You are not an Admin.");

            var user = await _userManager.FindByNameAsync(username);
            if(user==null) return NotFound("The user you tried to delete doesn't exist.");

            _ = await _userManager.DeleteAsync(user);
            return Ok("User "+ username +" succesfully deleted.");
        }
        [Authorize]
        [HttpGet("all-users")]
        [ProducesResponseType(typeof(UserListContainerDto), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<ActionResult<GetUserResponseDto>> GetAllUsers()
        {
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(User.Identity.Name));
            bool isAdmin = false;
            for (int i = 0; i < roles.Count(); i++)
            {
                if (roles[i] == adminRoleString)
                    isAdmin = true;
            }
            if (isAdmin == false)
                return Unauthorized("You are not an Admin.");

            UserListContainerDto respBody = new();
            respBody.Users = ((List<User>)await _userManager.GetUsersInRoleAsync(userRoleString)).Select(u => new UserDto(u)).ToList();
            return StatusCode((int)HttpStatusCode.OK,respBody);
        }
    }
}
