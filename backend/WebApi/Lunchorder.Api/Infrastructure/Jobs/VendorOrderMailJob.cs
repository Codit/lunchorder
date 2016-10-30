using System;
using FluentScheduler;
using Lunchorder.Common;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Infrastructure.Jobs
{
    public class VendorOrderMailJob : IJob
    {
        private readonly IEmailService _emailService;
        private readonly ICacheService _cacheService;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;

        public VendorOrderMailJob(IEmailService emailService, ICacheService cacheService, IDatabaseRepository databaseRepository, IConfigurationService configurationService, ILogger logger)
        {
            if (emailService == null) throw new ArgumentNullException(nameof(emailService));
            if (cacheService == null) throw new ArgumentNullException(nameof(cacheService));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _emailService = emailService;
            _cacheService = cacheService;
            _databaseRepository = databaseRepository;
            _configurationService = configurationService;
            _logger = logger;
        }

        public void Execute()
        {
            _logger.Info($"Vendor email schedule triggered at {DateTime.UtcNow} UTC");
            var now = DateTime.UtcNow;
            var dayOfWeek = now.DayOfWeek;

            // only execute on weekdays
            if ((dayOfWeek >= DayOfWeek.Monday) && (dayOfWeek <= DayOfWeek.Friday))
            {
                var menu = _cacheService.GetMenu();
                menu.Wait();

                // only execute if not closed
                foreach (var closedDate in menu.Result.Vendor.ClosingDateRanges)
                {
                    if (DateTime.Compare(now, DateTime.Parse(closedDate.From)) > 1 && DateTime.Compare(now, DateTime.Parse(closedDate.Until)) < 1)
                    {
                        return;
                    }
                }

                var dateTime = DateTime.UtcNow;
                var vendorOrderHistory = _databaseRepository.GetVendorOrder(new DateGenerator().GenerateDateFormat(dateTime), menu.Result.Vendor.Id);
                vendorOrderHistory.Wait();

                // only submit if not submitted before
                if (vendorOrderHistory.Result != null && !vendorOrderHistory.Result.Submitted)
                {
                    var htmlOutput = HtmlHelper.CreateVendorHistory(_configurationService, vendorOrderHistory.Result);

                    _emailService.SendHtmlEmail($"{_configurationService.Company.Name} order for {dateTime.ToString("D")}", menu.Result.Vendor.Address.Email, htmlOutput).Wait();
                    _logger.Info($"Vendor email sent at {DateTime.UtcNow}");
                    _databaseRepository.MarkVendorOrderAsComplete(vendorOrderHistory.Result.Id);
                }
            }
        }
    }
}