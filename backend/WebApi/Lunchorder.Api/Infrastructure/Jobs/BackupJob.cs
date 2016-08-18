using System;
using System.Diagnostics;
using FluentScheduler;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Infrastructure.Jobs
{
    public class BackupJob : IJob
    {
        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;

        public BackupJob(IConfigurationService configurationService, ILogger logger)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _configurationService = configurationService;
            _logger = logger;
        }

        public void Execute()
        {
            _logger.Info($"Backup Job schedule triggered at {DateTime.UtcNow}");
            var now = DateTime.UtcNow;
            var dayOfWeek = now.DayOfWeek;

            Process process = new Process
            {
                StartInfo =
                {
                    FileName = "CVS.exe",
                    Arguments = "if any"
                }
            };
            process.Start();
        }
    }
}