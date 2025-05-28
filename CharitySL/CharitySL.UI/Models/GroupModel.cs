using System.ComponentModel.DataAnnotations;

namespace CharitySL.UI.Models
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
		[Required(ErrorMessage = "Please enter the group name.")]
		public string Name { get; set; }

		public string? Description { get; set; }

		public IReadOnlyCollection<string>? AssignedUsers { get; set; }
	}

	public class UpdateGroupRequest : CreateProjectRequest
	{
	}
}
