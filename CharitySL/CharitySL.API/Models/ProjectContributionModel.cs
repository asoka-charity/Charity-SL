using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Models
{
	public class CreateProjectContributionRequest
	{
		public string UserId { get; set; }
		[Required(ErrorMessage = "Please enter an amount")]
		public decimal Amount { get; set; }
		public DateTime? PaidDate { get; set; }
		public string? PaymentMethod	{ get; set; }
		public string? PaymentStatus { get; set; }
		public string? PaymentReference { get; set; }
		public string? AdditionalNote { get; set; }
	}

	public class CreateProjectContributionResponse
	{
		public int Id { get; set; }
	}

	public class DonationModel
	{
		public int Id { get; set; }
		public string DonatedUserName { get; set; }
		public string? DonatedUserEmail { get; set; }
		public decimal DonatedAmount { get; set; }
		public decimal? ActualAmount { get; set; }
		public string DonationMethod { get; set; }
		public string DonationStatus { get; set; }
		public DateTime? PaidDate { get; set; }
		public string? DonationReference { get; set; }
		public string? DonationNote { get; set; }
	}

	public class DonationDetailModel
	{
		public int Id { get; set; }
		public UserModel DonatedUser { get; set; }
		public ProjectModel DonatedProject { get; set; }
		public decimal DonatedAmount { get; set; }
		public decimal? ActualAmount { get; set; }
		public string? DonationMethod { get; set; }
		public string? DonationStatus { get; set; }
		public string? DonationReference { get; set; }
		public string? DonationNote { get; set; }
		public DateTime? PaidDate { get; set; }
		public string? DonatedOn { get; set; }
	}

	public class UpdateProjectContribution : DonationDetailModel
	{

	}
}
