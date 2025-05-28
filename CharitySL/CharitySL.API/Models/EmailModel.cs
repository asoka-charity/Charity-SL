namespace CharitySL.API.Models
{ 
	public class UserEmailModel
	{
		public string Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
	}

	public class SendEmailRequest
	{
		public List<string> Recipients { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string? TemplateName { get; set; }
		public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
	}

	public class EmailAttachment
	{
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public byte[] Content { get; set; }
	}

	public class SentEmailDetails
	{
		public int Id { get; set; }
		public string Receipient { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBodyHtml { get; set; }
		public string EmailStatus { get; set; }
		public string EmailSentOn { get; set; }
	}
}
