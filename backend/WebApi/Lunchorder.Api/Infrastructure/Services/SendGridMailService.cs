using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;
using Lunchorder.Common.Interfaces;
using NLog;
using SendGrid;
using SendGrid.Helpers.Mail;
using ILogger = Lunchorder.Common.Interfaces.ILogger;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class NLogLogger : ILogger
    {
        private readonly Logger logger;

        public NLogLogger(Type loggerType)
        {
            if (loggerType == null) throw new ArgumentNullException(nameof(loggerType));
            logger = LogManager.GetLogger(loggerType.FullName);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.ErrorException(message, exception);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            logger.FatalException(message, exception);
        }
    }

    public class SendGridMailService : IEmailService
    {
        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;

        public SendGridMailService(IConfigurationService configurationService, ILogger logger)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _configurationService = configurationService;
            _logger = logger;
        }

        public async Task SendHtmlEmail(string subject, string toEmail, string content)
        {
            string apiKey = _configurationService.Email.ApiKey;
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email(_configurationService.Email.From);
            Email to = new Email(toEmail);
            Content mailContent = new Content("text/html", content);
            Mail mail = new Mail(from, subject, to, mailContent);

            var personalization = new Personalization();
            personalization.AddTo(to);

            if (_configurationService.Email.Bcc != null && _configurationService.Email.Bcc.Any())
            {

                foreach (var bcc in _configurationService.Email.Bcc)
                {
                    personalization.AddBcc(new Email(bcc));
                }
                mail.AddPersonalization(personalization);
            }

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                var responseMsg = response.Body.ReadAsStringAsync().Result;
                _logger.Error($"Unable to send email: {responseMsg}");
            }
        }
    }
}