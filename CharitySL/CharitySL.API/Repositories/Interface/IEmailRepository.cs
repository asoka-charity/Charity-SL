using CharitySL.API.Models;

namespace CharitySL.API.Repositories.Interface
{
	public interface IEmailRepository
	{
		Task<IEnumerable<UserEmailModel>> GetEmailUsers();
		List<string> GetEmailTemplateList();
		string GetTemplateContent(string templateName);
		Task SendEmailAsync(SendEmailRequest request);
		Task<IEnumerable<SentEmailDetails>> GetSentEmails();
	}
}
