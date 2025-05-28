using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Models
{
	public class GroupModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
	}

	public class GroupDetailsModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public IEnumerable<string>? AssignedUsers { get; set; }
		public IReadOnlyCollection<string>? AssignedUserIds { get; set; }
		public string CreatedAt { get; set; }
	}

	public class AssignedUserModel
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}

	public class CreateGroupRequest
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		public IReadOnlyCollection<string>? AssignedUsers { get; set; }
	}

	public class CreateGroupResponse
	{
		public int Id { get; set; }
	}

	public class UpdateGroupRequest : GroupDetailsModel
	{
	}
}
