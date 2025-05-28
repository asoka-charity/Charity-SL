using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectContributionController : ControllerBase
	{
		private readonly IProjectContributionService _projectContributionService;

		public ProjectContributionController(IProjectContributionService projectContributionService)
		{
			_projectContributionService = projectContributionService;
		}

		[HttpGet("{projectId}", Name = "GetProjectContributions")]
		[ProducesResponseType(typeof(IEnumerable<DonationModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProjectContributions([FromRoute] int projectId)
		{
			var contributions = _projectContributionService.GetProjectContributions(projectId);
			return Ok(contributions);
		}

		[HttpPost("{projectId}", Name = "CreateProjectContributions")]
		[ProducesResponseType(typeof(CreateProjectContributionResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateProjectContributions([FromRoute] int projectId, [FromBody] CreateProjectContributionRequest createProjectContributionRequest)
		{
			return Ok(_projectContributionService.CreateProjectContributions(projectId, createProjectContributionRequest, "ADMIN"));
		}

		[HttpGet("{id}/details", Name = "GetProjectContributionDetails")]
		[ProducesResponseType(typeof(DonationDetailModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProjectContributionDetails([FromRoute] int id)
		{
			return Ok(_projectContributionService.GetProjectContributionDetails(id));
		}

		[HttpPut("{id}/details", Name = "UpdateProjectContribution")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateProjectContribution([FromRoute] int id, [FromBody] UpdateProjectContribution updateRequest)
		{
			_projectContributionService.UpdateProjectContribution(id, updateRequest);
			return Ok();
		}

		[HttpDelete("{id}/details", Name = "DeleteProjectContribution")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteProjectContribution([FromRoute] int id)
		{
			_projectContributionService.DeleteProjectContribution(id);
			return Ok();
		}
	}
}
