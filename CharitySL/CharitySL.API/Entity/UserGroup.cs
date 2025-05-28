using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class UserGroup : BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(450)]
		public string UserId { get; set; }

		public int GroupId { get; set; }

		public virtual User User { get; set; }

		public virtual Group Group { get; set; }
	}
}
