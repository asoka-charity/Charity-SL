using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Models
{
	public class ProjectOwnerModel
	{
		public int ProjectId { get; set; }
		public List<ProjectOwnerDetailModel> ProjectOwnerDetails { get; set; }
	}

	public class ProjectOwnerDetailModel
	{
		public string OwnerId { get; set; }
		public string OwnerFirstName { get; set; }
		public string? OwnerLastName { get; set; }
		public string? OwnerEmail { get; set; }
		public string OwnerGuid { get; set; }
	}

	public class CreateProjectOwnerRequest
	{
		[Required]
		public List<string> UserIds { get; set; }
	}
}
