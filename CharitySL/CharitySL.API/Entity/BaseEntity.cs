namespace CharitySL.API.Entity
{
	public class BaseEntity
	{
		public bool IsDeleted { get; set; }

		public DateTimeOffset CreatedAt { get; internal set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public BaseEntity()
		{
			CreatedAt = DateTimeOffset.UtcNow;
		}
	}
}
