using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using MenuVendorClosingDateRange = Lunchorder.Domain.Dtos.MenuVendorClosingDateRange;

namespace Lunchorder.Common
{
    public class JobService : IJobService
    {
        private readonly IPushTokenService _pushTokenService;
        private readonly ICacheService _cacheService;

        public JobService(ICacheService cacheService, IPushTokenService pushTokenService)
        {
            _pushTokenService = pushTokenService ?? throw new ArgumentNullException(nameof(pushTokenService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task RemindUsers()
        {
            var utcNow = DateTime.UtcNow;
            if (IsWeekDay(utcNow))
            {
                var menu = await _cacheService.GetMenu();

                if (!IsVenueOpen(utcNow, menu.Vendor.ClosingDateRanges))
                {
                    return;
                }

                await _pushTokenService.SendPushNotification();
            }
        }

        /// <summary>
        /// Checks if the current datetime is a weekday
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private bool IsWeekDay(DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            return (dayOfWeek >= DayOfWeek.Monday) && (dayOfWeek <= DayOfWeek.Friday);
        }

        /// <summary>
        /// Checks if venue is not closed at a given datetime
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="vendorClosingDateRanges"></param>
        /// <returns></returns>
        private bool IsVenueOpen(DateTime currentTime, IEnumerable<MenuVendorClosingDateRange> vendorClosingDateRanges)
        {
            foreach (var closedDate in vendorClosingDateRanges)
            {
                if (DateTime.Compare(currentTime, DateTime.Parse(closedDate.From)) > 1 && DateTime.Compare(currentTime, DateTime.Parse(closedDate.Until)) < 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
