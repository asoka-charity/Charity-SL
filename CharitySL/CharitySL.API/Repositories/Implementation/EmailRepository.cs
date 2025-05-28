using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace CharitySL.API.Repositories.Implementation
{
	public class EmailRepository : BaseRepo, IEmailRepository
	{
		private readonly string _templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");
		private readonly IConfiguration _configuration;

		public EmailRepository(AppDbContext context, IConfiguration configuration) : base(context)
		{
			_configuration = configuration;
		}

		public async Task<IEnumerable<UserEmailModel>> GetEmailUsers()
		{
			return await _context.Users
								 .Where(u => u.Role == "USER" && !string.IsNullOrEmpty(u.Email) && !u.IsDeleted)
								 .Select(q => new UserEmailModel()
								 {
									 Id = q.Id.ToString(),
									 Email = q.Email,
									 FirstName = q.FirstName,
									 LastName = q.LastName,
								 }).ToListAsync();
		}

		public List<string> GetEmailTemplateList()
		{
			if (!Directory.Exists(_templatePath))
			{
				throw new InvalidOperationException("Templates folder not found.");
			}

			var templates = Directory.GetFiles(_templatePath, "*.html").Select(Path.GetFileName).ToList();

			if (templates == null || templates.Count == 0)
				throw new InvalidOperationException("No templates found.");

			return templates;
		}

		public string GetTemplateContent(string templateName)
		{
			string filePath = Path.Combine(_templatePath, templateName);

			if (!System.IO.File.Exists(filePath))
			{
				throw new InvalidOperationException("Template not found.");
			}

			string content = System.IO.File.ReadAllText(filePath);

			if (string.IsNullOrEmpty(content))
				throw new InvalidOperationException("Template content is empty.");

			return content;
		}

		public async Task SendEmailAsync(SendEmailRequest request)
		{
			if (request == null || request.Recipients.Count == 0)
				throw new InvalidOperationException("Invalid request");

			if (!string.IsNullOrEmpty(request.TemplateName))
			{
				if (string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
					throw new InvalidOperationException("Subject and body are required");
			}

			var fromAddress = new MailAddress(_configuration["EmailSettings:FromAddress"], _configuration["EmailSettings:FromName"]);
			var fromPassword = _configuration["EmailSettings:FromPassword"];

			var emailAuditList = new List<EmailAudit>();

			foreach (var recipientEmail in request.Recipients)
			{
				string emailBody = request.Body;
				var subject = request.Subject;

				var recipient = _context.Users.FirstOrDefault(u => u.Email == recipientEmail);
				if (recipient == null)
				{
					emailAuditList.Add(new EmailAudit
					{
						EmailSubject = subject,
						EmailBody = emailBody,
						EmailReceipient = recipientEmail,
						EmailStatus = "Failed! Recipient not found"
					});
					continue;
				}

				if (!string.IsNullOrEmpty(request.TemplateName))
				{
					if (request.TemplateName == "WelcomeEmail.html")
					{
						subject = "Welcome to CharitySL";

						string verificationLink = $"{_configuration["EmailSettings:VerificationLink"]}/{recipient.Guid}";
						emailBody = emailBody.Replace("{RecipientName}", string.Concat(recipient.FirstName, recipient.LastName))
											 .Replace("{VerificationLink}", verificationLink);
					}
				}

				try
				{
					var message = new MailMessage(fromAddress, new MailAddress(recipient.Email))
					{
						Subject = subject,
						Body = emailBody,
						IsBodyHtml = true
					};

					// Add attachments if any
					if (request.Attachments != null && request.Attachments.Any())
					{
						foreach (var attachment in request.Attachments)
						{
							var stream = new MemoryStream(attachment.Content);
							var mailAttachment = new Attachment(stream, attachment.FileName, attachment.ContentType);
							message.Attachments.Add(mailAttachment);
						}
					}

					var smtp = new SmtpClient
					{
						Host = "smtp.gmail.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
					};
					
					await smtp.SendMailAsync(message);

					emailAuditList.Add(new EmailAudit
					{
						EmailSubject = subject,
						EmailBody = emailBody,
						EmailReceipient = recipientEmail,
						EmailStatus = "Sent"
					});
					continue;
				}
				catch (SmtpFailedRecipientException ex)
				{
					emailAuditList.Add(new EmailAudit
					{
						EmailSubject = subject,
						EmailBody = emailBody,
						EmailReceipient = recipientEmail,
						EmailStatus = "Failed"
					});
					continue;
				}
				catch (SmtpException ex)
				{
					emailAuditList.Add(new EmailAudit
					{
						EmailSubject = subject,
						EmailBody = emailBody,
						EmailReceipient = recipientEmail,
						EmailStatus = "Failed"
					});
					continue;
				}
				catch (Exception ex)
				{
					emailAuditList.Add(new EmailAudit
					{
						EmailSubject = subject,
						EmailBody = emailBody,
						EmailReceipient = recipientEmail,
						EmailStatus = "Failed"
					});
					continue;
				}
			}

			_context.EmailAudits.AddRange(emailAuditList);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<SentEmailDetails>> GetSentEmails()
		{
			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return await _context.EmailAudits
								 .Where(u => !u.IsDeleted)
								 .Select(q => new SentEmailDetails()
								 {
									 Id = q.Id,
									 Receipient = q.EmailReceipient,
									 EmailSubject = q.EmailSubject,
									 EmailBodyHtml = q.EmailBody,
									 EmailStatus = q.EmailStatus,
									 EmailSentOn = TimeZoneInfo.ConvertTimeFromUtc(q.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
								 }).ToListAsync();
		}
	}
}
