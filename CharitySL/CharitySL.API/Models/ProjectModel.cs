using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Models
{
	public class ProjectModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Status { get; set; }
		public string? Category { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
	}

	public class ProjectDetailModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Status { get; set; }
		public string? Category { get; set; }
		public string CreatedAt { get; set; }
	}

	public class CreateProjectRequest
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Status { get; set; }
		public string? Category { get; set; }
	}

	public class CreateProjectResponse
	{
		public int Id { get; set; }
	}

	public class UpdateProjectRequest : CreateProjectRequest
	{
	}

	public class UserProjectModel
	{
		public string Guid { get; set; }
		public List<ProjectModel> ProjectsList { get; set; }
	}
}
