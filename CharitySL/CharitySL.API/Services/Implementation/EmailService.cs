using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class EmailService : IEmailService
	{
		private readonly IEmailRepository _emailRepository;

		public EmailService(IEmailRepository emailRepository)
		{
			_emailRepository = emailRepository;
		}

		public async Task<IEnumerable<UserEmailModel>> GetEmailUsers()
		{
			return await _emailRepository.GetEmailUsers();
		}

		public List<string> GetEmailTemplateList()
		{
			return _emailRepository.GetEmailTemplateList();
		}

		public string GetTemplateContent(string templateName)
		{
			return _emailRepository.GetTemplateContent(templateName);
		}

		public Task SendEmailAsync(SendEmailRequest request)
		{
			return _emailRepository.SendEmailAsync(request);
		}

		public async Task<IEnumerable<SentEmailDetails>> GetSentEmails()
		{
			return await _emailRepository.GetSentEmails();
		}
	}
}
