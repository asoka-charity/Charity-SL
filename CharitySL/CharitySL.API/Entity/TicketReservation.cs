using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class TicketReservation : BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(200)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string? Email {  get; set; }

		[MaxLength(200)]
		public string? PhoneNumber { get; set; }

		public int NumberOfTickets { get; set; }

		public bool IsStudent { get; set; } = false;

		[MaxLength(200)]
		public string? ContactedBy { get; set; }

		[MaxLength(500)]
		public string? Comments { get; set; }
	}
}
