using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.DocumentDb;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Services
{
    [TestFixture]
    public class JobServiceTest : ServiceBase
    {
        private readonly string _routePrefix = "menus";

        [Test]
        [Ignore]
        public async Task Get()
        {
            var pushTokenItems = new List<PushTokenItem>();
            pushTokenItems.Add(new PushTokenItem { Token = "TOKEN_FROM_CONFIG_HERE_TODO" });
            var mockedDatabaseRepository = new Mock<IDatabaseRepository>();
            mockedDatabaseRepository.Setup(x => x.GetPushTokens()).ReturnsAsync(pushTokenItems);
            var mockedConfigurationService = new Mock<IConfigurationService>();

            var jobService = new JobService(CacheService, mockedDatabaseRepository.Object, mockedConfigurationService.Object);
            await jobService.RemindUsers();
        }
    }
}