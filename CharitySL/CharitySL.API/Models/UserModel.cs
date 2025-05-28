using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Models
{
	public class UserModel
	{
		public string Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? WhatsAppNumber { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
	}

	public class UserDetailsModel : AddressModel
	{
		public string Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? WhatsAppNumber { get; set; }
		public string? Guid { get; set; }
		public string? Description { get; set; }
		public string? UserType { get; set; }
		public bool IsActive { get; set; }
		public string? IntroducedBy { get; set; }
		public IEnumerable<string>? AssignedGroups { get; set; }
		public IReadOnlyCollection<int>? GroupIds { get; set; }
		public string CreatedAt { get; set; }
	}

	public class AddressModel
	{
		public string? Street { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		public string? Province { get; set; }
		public string? PostalCode { get; set; }
	}

	public class CreateUserRequest : AddressModel
	{
		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[MaxLength(100)]
		public string? LastName { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		[Phone]
		public string? PhoneNumber { get; set; }

		[Phone]
		public string? WhatsAppNumber { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }

		public string? UserType { get; set; }

		[DefaultValue(true)]
		public bool IsActive { get; set; }

		[MaxLength(300)]
		public string? IntroducedBy { get; set; }

		public IEnumerable<int>? GroupList { get; set; }
	}

	public class CreateUserResponse
	{
		public string Id { get; set; }
	}

	public class UpdateUserRequest : UserDetailsModel
	{
		[Required]
		public string Id { get; set; }
	}

	public class UserData
	{
		public string DisplayName { get; set; }
	}
}
