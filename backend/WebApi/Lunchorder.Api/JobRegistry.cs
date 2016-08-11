using System;
using Castle.Windsor;
using FluentScheduler;
using Lunchorder.Api.Infrastructure.Jobs;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api
{
    public class JobRegistry : Registry
    {
        public JobRegistry(IWindsorContainer container)
        {
            var cacheService = container.Resolve<ICacheService>();

            var menu = cacheService.GetMenu();
            menu.Wait();
            DateTime submitOrderTime;

            if (DateTime.TryParse(menu.Result.Vendor.SubmitOrderTime, out submitOrderTime) && menu.Exception == null)
            {
                // replace the submitOrderTime (that is a datetime, no timespan) with the current day and keep the same HH:MM:SS).
                TimeSpan ts = new TimeSpan(submitOrderTime.Hour, submitOrderTime.Minute, submitOrderTime.Second);
                submitOrderTime = DateTime.UtcNow.Date + ts;
                Schedule<VendorOrderMailJob>().ToRunOnceAt(submitOrderTime).AndEvery(1).Days();
            }
        }
    }
}