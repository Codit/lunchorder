using System;
using Castle.Windsor;
using FluentScheduler;
using Lunchorder.Api.Infrastructure.Jobs;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api
{
    public class JobRegistry : Registry
    {
        public JobRegistry(IWindsorContainer container)
        {
            var cacheService = container.Resolve<ICacheService>();
            var configurationService = container.Resolve<IConfigurationService>();
            var logger = container.Resolve<ILogger>();

            foreach (var job in configurationService.Jobs)
            {
                if (job.Enabled)
                {
                    switch (job.Name)
                    {
                        case "email":
                            var menu = cacheService.GetMenu();
                            menu.Wait();
                            DateTime submitOrderTime;

                            if (DateTime.TryParse(menu.Result.Vendor.SubmitOrderTime, out submitOrderTime) &&
                                menu.Exception == null)
                            {
                                Schedule<VendorOrderMailJob>().ToRunEvery(0).Days().At(submitOrderTime.Hour, submitOrderTime.Minute);
                                logger.Info($"Order mail job has been scheduled to run every day @ {submitOrderTime.Hour}h{submitOrderTime.Minute}s UTC");
                            }
                            break;
                        case "backup":
                            var hours = 1;
                            var seconds = 0;
                            // run each first hour of the day
                            Schedule<BackupJob>().ToRunEvery(0).Days().At(1,0);
                            logger.Info($"Backup job has been scheduled to run every day @ {hours}h{seconds}s UTC");
                            break;
                    }
                }
            }
        }
    }
}