using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class Group : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string Name { get; set; }

		[MaxLength(400)]
		public string? Description { get; set; }

		public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
	}
}
