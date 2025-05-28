using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class ProjectOwner : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		public int ProjectId { get; set; }

		[Required]
		public string UserId { get; set; }

		public virtual Project Project { get; set; }
		public virtual User User { get; set; }
	}
}
