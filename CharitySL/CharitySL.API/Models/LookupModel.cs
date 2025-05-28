namespace CharitySL.API.Models
{
	public class LookupDataResponse
	{
		public List<UserTypeModel> UserTypes { get; set; }
		public List<ProjectStatusModel> ProjectStatuses { get; set; }
		public List<ProjectCategoryModel> ProjectCategories { get; set; }
		public List<PaymentStatusModel> PaymentStatuses { get; set; }
		public List<PaymentMethodModel> PaymentMethods { get; set; }
		public List<string> Countries { get; set; }
	}

	public class UserTypeModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ProjectStatusModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ProjectCategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class PaymentStatusModel
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}

	public class PaymentMethodModel
	{
		public int Id { get; set; }
		public string MethodName { get; set; }
	}

	public class CountryModel
	{
		public int Id { get; set; }
		public string Country { get; set; }
	}
}
