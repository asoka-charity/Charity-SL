namespace CharitySL.API.Entity
{
	public class EmailAudit : BaseEntity
	{
		public int Id { get; set; }
		public string? EmailReceipient { get; set; }
		public string? EmailSubject { get; set; }
		public string? EmailBody { get; set; }
		public string? EmailStatus { get; set; }
	}
}
