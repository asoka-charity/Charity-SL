using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharitySL.API.Entity
{
	public class ProjectContribution : BaseEntity
	{
		public int Id { get; set; }

		[Required]
		public int ProjectOwnerId { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal? ActualAmount { get; set; }

		public DateTimeOffset? PaidDate { get; set; }

		[Required]
		public int PaymentMethodId { get; set; }

		[MaxLength(200)]
		public string? PaymentReference { get; set; }

		[Required]
		public int PaymentStatusId { get; set; }

		[MaxLength(200)]
		public string? AdditionalNote { get; set; }

		public virtual ProjectOwner ProjectOwner { get; set; }
		public virtual PaymentMethod PaymentMethod { get; set; }
		public virtual PaymentStatus PaymentStatus { get; set; }
	}
}
