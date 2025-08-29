
using System.Net.Mail;
using System.Net;
using ThieuNhiTT.Web.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ThieuNhiTT.Web.Services
{
	public class EmailService : IEmailService, IEmailSender
	{
		public string smtpHost { get;set; }
		public int smtpPort { get;set; }
		public string smtpUsername { get;set; }
		public string smtEmail { get; set; }
		public string displayName { get; set; }

		public string smtpPassword { get;set; }
		public bool enableSsl { get;set; }
		private readonly EmailSenderOptions _emailOptions;
		private readonly ILogger<EmailService> _logger;
		public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.


		public EmailService(IOptions<EmailSenderOptions> emailOptions, IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailService> logger)
    {
						Options = optionsAccessor.Value;
						_emailOptions = emailOptions.Value;
						_logger = logger;
            smtpHost = _emailOptions.Host;
						smtEmail = _emailOptions.Email;
						smtpPort = _emailOptions.Port;
						smtpUsername = _emailOptions.Username;
						displayName = _emailOptions.DisplayName;
						smtpPassword = _emailOptions.Password;
						enableSsl = _emailOptions.EnableSSL;
    }


		public async Task Execute(string apiKey, string subject, string message, string toEmail)  
		{
			_logger.LogDebug("Preparing to send email to {ToEmail} with subject {Subject}", toEmail, subject);
			
			var client = new SendGridClient(apiKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("Joe@contoso.com", "Password Recovery"),
				Subject = subject,
				PlainTextContent = message,
				HtmlContent = message
			};
			msg.AddTo(new EmailAddress(toEmail));

			// Disable click tracking.
			// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
			msg.SetClickTracking(false, false);
			
			try
			{
				var response = await client.SendEmailAsync(msg);
				_logger.LogInformation("Email sent successfully to {ToEmail} with status {StatusCode}", toEmail, response.StatusCode);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to send email to {ToEmail} with subject {Subject}", toEmail, subject);
				throw;
			}
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			if (string.IsNullOrEmpty(Options.SendGridKey))
			{
				throw new Exception("Null SendGridKey");
			}
			await Execute(Options.SendGridKey, subject, htmlMessage, email);
		}

		public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, string fromEmail = null)
		{
			try
			{
				if (string.IsNullOrEmpty(Options.SendGridKey))
				{
					throw new Exception("Null SendGridKey");
				}
				await Execute(Options.SendGridKey, subject, body, toEmail);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
