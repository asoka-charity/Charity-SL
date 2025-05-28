using Microsoft.AspNetCore.Mvc;
using CharitySL.API.Services.Interface;
using CharitySL.API.Models;

namespace CharitySL.API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpGet("users", Name = "GetEmailUsers")]
		[ProducesResponseType(typeof(IEnumerable<UserEmailModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetEmailUsers()
		{
			var users = await _emailService.GetEmailUsers();
			return Ok(users);
		}

		[HttpGet("template/list", Name = "GetEmailTemplates")]
		public IActionResult GetTemplateList()
		{
			var templates = _emailService.GetEmailTemplateList();
			return Ok(templates);
		}

		[HttpGet("template/{templateName}", Name = "GetEmailTemplatesContentByName")]
		public IActionResult GetTemplateContent(string templateName)
		{
			string content = _emailService.GetTemplateContent(templateName);
			return Ok(content);
		}

		[HttpPost("send", Name = "SendEmail")]
		public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
		{
			await _emailService.SendEmailAsync(request);
			return Ok("Email sent successfully.");
		}

		[HttpGet("sent", Name = "GetSentEmails")]
		[ProducesResponseType(typeof(IEnumerable<SentEmailDetails>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetSentEmails()
		{
			var emails = await _emailService.GetSentEmails();
			return Ok(emails);
		}
	}
}
