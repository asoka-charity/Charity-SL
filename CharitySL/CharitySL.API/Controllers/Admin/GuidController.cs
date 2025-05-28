using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class GuidController : ControllerBase
	{
		private readonly IUserService _userService;

		public GuidController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPut(Name = "UpdateUserGuids")]
		public IActionResult UpdateUserGuids([FromBody] List<string> guidIds)
		{
			_userService.UpdateUserGuids(guidIds);
			return Ok("User GUIDs updated successfully.");
		}
	}
}
