using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CharitySL.API.Entity
{
	public class User : IdentityUser
	{
		[MaxLength(100)]
		public string? FirstName { get; set; }

		[MaxLength(100)]
		public string? LastName { get; set; }

		[MaxLength(200)]
		public string Guid { get; set; }

		[MaxLength(50)]
		public string? Role { get; set; }

		[MaxLength(200)]
		public string? Street { get; set; }

		[MaxLength(200)]
		public string? City { get; set; }

		public int? CountryId { get; set; }

		public int? ProvinceId { get; set; }

		[MaxLength(100)]
		public string? PostalCode { get; set; }

		[MaxLength(50)]
		public string? WhatsAppNumber { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }

		public bool IsDeleted { get; set; }

		public int? UserTypeId { get; set; }

		public bool IsActive { get; set; } = true;

		[MaxLength(300)]
		public string? IntroducedBy { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public virtual UserType? UserType { get; set; }

		public virtual Country? Country { get; set; }

		public virtual Province? Province { get; set; }

		public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
	}
}
