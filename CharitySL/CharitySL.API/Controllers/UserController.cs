
using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Services.Implementation;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IUserService _userService;
		private readonly IProjectContributionService _projectContributionService;

		public UserController(AppDbContext context, IUserService userService, IProjectContributionService projectContributionService)
		{
			_context = context;
			_userService = userService;
			_projectContributionService = projectContributionService;
		}

		[HttpGet("{guid}")]
		public async Task<IActionResult> Get(string guid)
		{
			if (!Guid.TryParse(guid, out _))
			{
				return BadRequest("Invalid GUID format.");
			}

			var userDetails = await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid && u.Role == "USER");
			if (userDetails != null)
			{
				return Ok(new UserData
				{
					DisplayName = userDetails.FirstName + " " + userDetails.LastName,
				});
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet("{guid}/projects", Name = "GetProjectsByUser")]
		[ProducesResponseType(typeof(UserProjectModel), StatusCodes.Status200OK)]
		public IActionResult GetProjectsByUser(string guid)
		{
			string sanitized = guid.Trim('\\', '"');
			var users = _userService.GetProjectsByUser(sanitized);
			return Ok(users);
		}

		[HttpPost("{guid}/contribution/{projectId}", Name = "CreateProjectContributionsByUser")]
		[ProducesResponseType(typeof(CreateProjectContributionResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateProjectContributionsByUser([FromRoute] int projectId, [FromBody] CreateProjectContributionRequest createProjectContributionRequest)
		{
			return Ok(_projectContributionService.CreateProjectContributions(projectId, createProjectContributionRequest, "USER"));
		}
	}
}
