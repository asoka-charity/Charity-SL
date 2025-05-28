using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet(Name ="GetUsers")]
		[ProducesResponseType(typeof(IEnumerable<UserModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _userService.GetUsersAsync();
			return Ok(users);
		}

		[HttpGet("profile/{userId}", Name = "GetUserDetails")]
		[ProducesResponseType(typeof(UserDetailsModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetUserDetails([FromRoute] string userId, [FromQuery] string? role)
		{
			string sanitized = userId;

			if (role == "ADMIN")
			{
				sanitized = userId.Trim('\\', '"');
			}

			return Ok(_userService.GetUserDetails(sanitized, role));
		}

		[HttpGet("list", Name = "GetUserList")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetUserList()
		{
			var users = _userService.GetUserList();
			return Ok(users);
		}

		[HttpPost(Name = "CreateUser")]
		[ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateUser([FromBody] CreateUserRequest createUserRequest)
		{
			return Ok(_userService.CreateUser(createUserRequest));
		}

		[HttpPut("{userId}", Name = "UpdateUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateUser([FromRoute] string userId, [FromBody] UpdateUserRequest updateUserRequest)
		{
			_userService.UpdateUser(userId, updateUserRequest);
			return Ok();
		}

		[HttpDelete("{userId}", Name = "DeleteUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteUser([FromRoute] string userId)
		{
			_userService.DeleteUser(userId);
			return Ok();
		}

		[HttpGet("type/{userType}", Name = "GetUsersByUserTypeAsync")]
		[ProducesResponseType(typeof(IEnumerable<UserModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetUsersByUserTypeAsync([FromRoute] string userType)
		{
			return Ok(_userService.GetUsersByUserTypeAsync(userType));
		}

		[HttpGet("email/{email}", Name = "GetUserDetailsByEmail")]
		[ProducesResponseType(typeof(UserDetailsModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetUserDetailsByEmail([FromRoute] string email)
		{
			return Ok(_userService.GetUserDetailsByEmail(email));
		}
	}
}
