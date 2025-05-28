using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class PaymentStatus : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Status { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
