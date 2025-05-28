using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class ProjectCategory : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
