using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class Province : BaseEntity
	{
		public int Id { get; set; }

		public int CountryId { get; set; }

		[MaxLength(200)]
		public string ProvinceName { get; set; }

		public virtual Country Country { get; set; }
	}
}
