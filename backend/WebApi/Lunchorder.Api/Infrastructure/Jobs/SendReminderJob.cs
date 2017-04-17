using System;
using FluentScheduler;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Infrastructure.Jobs
{
    /// <summary>
    /// Job that sends our notifications for users
    /// </summary>
    public class SendReminderJob : IJob
    {
        private readonly IJobService _jobService;
        private readonly ILogger _logger;

        public SendReminderJob(IJobService jobService, ILogger logger)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {
            _logger.Info($"Reminder schedule triggered at {DateTime.UtcNow} UTC");
            _jobService.RemindUsers();
        }
    }
}