using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class SendGridMailService : IEmailService
    {
        private readonly IConfigurationService _configurationService;

        public SendGridMailService(IConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _configurationService = configurationService;
        }

        public async Task SendHtmlEmail(string subject, string toEmail, string content)
        {
            string apiKey = _configurationService.Email.ApiKey;
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email(_configurationService.Email.From);
            Email to = new Email(toEmail);
            Content mailContent = new Content("text/html", content);
            Mail mail = new Mail(from, subject, to, mailContent);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}