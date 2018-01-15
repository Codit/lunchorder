using System;
using NLog;
using ILogger = Lunchorder.Common.Interfaces.ILogger;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(Type loggerType)
        {
            if (loggerType == null) throw new ArgumentNullException(nameof(loggerType));
            _logger = LogManager.GetLogger(loggerType.FullName);
        }

        public void Debug(string message)
        {
            Debug(message);
        }

        public void Trace(string message)
        {
            Trace(message);
        }

        public void Info(string message)
        {
            Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            Error(message);
        }

        public void Error(string message, Exception exception)
        {
            Error(message, exception);
        }

        public void Fatal(string message)
        {
            Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Fatal(message, exception);
        }
    }
}