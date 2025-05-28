using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectOwnerController : ControllerBase
	{
		private readonly IProjectOwnerService _projectOwnerService;

		public ProjectOwnerController(IProjectOwnerService projectOwnerService)
		{
			_projectOwnerService = projectOwnerService;
		}

		[HttpGet("{projectId}", Name = "GetProjectOwners")]
		[ProducesResponseType(typeof(ProjectOwnerModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProjectOwners([FromRoute] int projectId)
		{
			return Ok(_projectOwnerService.GetProjectOwners(projectId));
		}

		[HttpPost("{projectId}", Name = "CreateProjectOwners")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateProjectOwner([FromRoute] int projectId, [FromBody] CreateProjectOwnerRequest createProjectOwnerRequest)
		{
			_projectOwnerService.CreateProjectOwners(projectId, createProjectOwnerRequest);
			return Ok();
		}
	}
}
