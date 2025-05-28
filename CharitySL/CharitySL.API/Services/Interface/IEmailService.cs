using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface IEmailService
	{
		Task<IEnumerable<UserEmailModel>> GetEmailUsers();
		List<string> GetEmailTemplateList();
		string GetTemplateContent(string templateName);
		Task SendEmailAsync(SendEmailRequest request);
		Task<IEnumerable<SentEmailDetails>> GetSentEmails();
	}
}
