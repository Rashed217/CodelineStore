using System.Net.Mail;
using System.Net;

namespace CodelineStore.Helper
{
    public class SendEmail : ISendEmail
    {
        private readonly ILogger<SendEmail> _logger;
        private readonly IConfiguration _configuration;
        public SendEmail(ILogger<SendEmail> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var password = _configuration["EmailSettings:Password"];

                var client = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };

                var message = new MailMessage(fromEmail, toEmail, subject, body);
                await client.SendMailAsync(message);
                _logger.LogInformation($"Email successfully sent to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                throw new InvalidOperationException("An error occurred while sending the email. Please try again later.");
            }
        }
    }
}

