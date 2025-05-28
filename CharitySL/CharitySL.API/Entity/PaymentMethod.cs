using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class PaymentMethod : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string MethodName { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
