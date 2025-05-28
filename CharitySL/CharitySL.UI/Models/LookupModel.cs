namespace CharitySL.UI.Models
{
	public class LookupData
	{
		public List<UserType> UserTypes { get; set; }
		public List<ProjectStatus> ProjectStatuses { get; set; }
		public List<ProjectCategory> ProjectCategories { get; set; }
		public List<PaymentStatus> PaymentStatuses { get; set; }
		public List<PaymentMethod> PaymentMethods { get; set; }
		public List<string> Countries { get; set; }
	}

	public class UserType
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ProjectStatus
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ProjectCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class PaymentStatus
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}

	public class PaymentMethod
	{
		public int Id { get; set; }
		public string MethodName { get; set; }
	}
}
