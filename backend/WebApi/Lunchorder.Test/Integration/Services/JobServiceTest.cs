using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Common.Interfaces;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Services
{
    [TestFixture]
    public class JobServiceTest : ServiceBase
    {
        [Test]
        [Ignore]
        public async Task Get()
        {
            var pushTokenServiceMock = new Mock<IPushTokenService>();
            pushTokenServiceMock.Setup(x => x.SendPushNotification());

            var jobService = new JobService(CacheService, pushTokenServiceMock.Object);
            await jobService.RemindUsers();
        }
    }
}