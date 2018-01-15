using System;
using NLog;
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
}