using System;
using System.Linq;
using System.Threading.Tasks;
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
            Error(message, exception);
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
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(_configurationService.Email.From);
            var to = new EmailAddress(toEmail);
            var mail = MailHelper.CreateSingleEmail(from, to, subject, content, content);

            if (_configurationService.Email.Bcc != null && _configurationService.Email.Bcc.Any())
            {
                foreach (var bcc in _configurationService.Email.Bcc)
                {
                    mail.AddBcc(new EmailAddress(bcc));
                }
            }

            var response = await client.SendEmailAsync(mail);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                var responseMsg = response.Body.ReadAsStringAsync().Result;
                _logger.Error($"Unable to send email: {responseMsg}");
            }
            else
            {
                _logger.Info($"Email '{subject}' has been successfully sent to '{toEmail}' at {DateTime.UtcNow.ToLongTimeString()}");
            }
        }
    }
}