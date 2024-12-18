using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using HangfireExample.Settings;

namespace HangfireExample.Services
{

    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value; // Get the actual settings
            _logger = logger;
        }

        public async Task SendReportEmail(string userEmail, string reportFilePath)
        {
            _logger.LogInformation($"Sending email to {userEmail} with attachment {reportFilePath}");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail),
                Subject = "Your Report",
                Body = "Please find your report attached.",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(userEmail);
            mailMessage.Attachments.Add(new Attachment(reportFilePath));

            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                smtpClient.EnableSsl = true;

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Email sending failed: {ex.Message}");
                }
            }
        }
    }

}