using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class LookupContactedBy : BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(200)]
		public string Name { get; set; }
	}
}
