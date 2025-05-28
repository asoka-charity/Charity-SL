using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectService _projectService;

		public ProjectController(IProjectService projectService)
		{
			_projectService = projectService;
		}

		[HttpGet(Name = "GetProjectsAsync")]
		[ProducesResponseType(typeof(IEnumerable<ProjectModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetProjectsAsync()
		{
			var projects = await _projectService.GetProjectsAsync();
			return Ok(projects);
		}

		[HttpGet("pending", Name = "GetPendingProjectsAsync")]
		[ProducesResponseType(typeof(IEnumerable<ProjectModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetPendingProjectsAsync()
		{
			var projects = await _projectService.GetProjectsAsync("PENDING");
			return Ok(projects);
		}

		[HttpGet("{projectId}", Name = "GetProjectDetails")]
		[ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProjectDetails([FromRoute] int projectId)
		{
			return Ok(_projectService.GetProjectDetails(projectId));
		}

		[HttpPost(Name = "CreateProject")]
		[ProducesResponseType(typeof(CreateProjectResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateProject([FromBody] CreateProjectRequest createProjectRequest)
		{
			return Ok(_projectService.CreateProject(createProjectRequest));
		}

		[HttpPut("{projectId}", Name = "UpdateProject")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateProject([FromRoute] int projectId, [FromBody] UpdateProjectRequest updateProjectRequest)
		{
			_projectService.UpdateProject(projectId, updateProjectRequest);
			return Ok();
		}

		[HttpDelete("{projectId}", Name = "DeleteProject")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteProject([FromRoute] int projectId)
		{
			_projectService.DeleteProject(projectId);
			return Ok();
		}
	}
}
