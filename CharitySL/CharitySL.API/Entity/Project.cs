using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class Project: BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }

		public int? ProjectStatusId { get; set; }

		public int? ProjectCategoryId { get; set; }

		public virtual ProjectStatus? ProjectStatus { get; set; }
		public virtual ProjectCategory? ProjectCategory { get; set; }
	}
}
