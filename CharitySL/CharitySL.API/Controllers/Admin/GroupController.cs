using CharitySL.API.Models;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly IGroupService _groupService;

		public GroupController(IGroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpGet(Name = "GetGroupsAsync")]
		[ProducesResponseType(typeof(IEnumerable<GroupModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetGroupsAsync()
		{
			var groups = await _groupService.GetGroupsAsync();
			return Ok(groups);
		}

		[HttpGet("list", Name = "GetGroupNames")]
		[ProducesResponseType(typeof(IEnumerable<GroupModel>), StatusCodes.Status200OK)]
		public IActionResult GetGroupNames()
		{
			var groups = _groupService.GetGroupNames();
			return Ok(groups);
		}

		[HttpGet("{groupId}", Name = "GetGroupDetails")]
		[ProducesResponseType(typeof(GroupDetailsModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetGroupDetails([FromRoute] int groupId)
		{
			return Ok(_groupService.GetGroupDetails(groupId));
		}

		[HttpGet("{groupId}/users", Name = "GetAssignedUsers")]
		[ProducesResponseType(typeof(IEnumerable<AssignedUserModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAssignedUsers([FromRoute] int groupId)
		{
			var assignedUsers = await _groupService.GetAssignedUsers(groupId);
			return Ok(assignedUsers);
		}

		[HttpPost(Name = "CreateGroup")]
		[ProducesResponseType(typeof(CreateGroupResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateGroup([FromBody] CreateGroupRequest createGroupRequest)
		{
			return Ok(_groupService.CreateGroup(createGroupRequest));
		}

		[HttpPut("{groupId}", Name = "UpdateGroup")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateGroup([FromRoute] int groupId, [FromBody] UpdateGroupRequest updateGroupRequest)
		{
			_groupService.UpdateGroup(groupId, updateGroupRequest);
			return Ok();
		}

		[HttpDelete("{groupId}", Name = "DeleteGroup")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteGroup([FromRoute] int groupId)
		{
			_groupService.DeleteGroup(groupId);
			return Ok();
		}
	}
}
