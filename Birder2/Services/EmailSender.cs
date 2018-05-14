using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class EmailSender : IEmailSender
    {
        //public AuthMessageSenderOptions Options { get; } //Development only, set via Secret Manager
        private readonly IConfiguration _configuration = null;

        public EmailSender(IConfiguration configuration) //IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _configuration = configuration;
            //Options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(_configuration["BirderSendGrid"], subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("DoNotReply@Birder.Com", "Birder Website"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
