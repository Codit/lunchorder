using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Windsor;
using Lunchorder.Api.Configuration.IoC;
using Lunchorder.Api.Infrastructure.Services;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.DocumentDb;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Unit.Services
{
    [TestFixture]
    public class PushTokenServiceTest
    {
        [Test]
        public async Task SendPushNotification()
        {
            var databaseRepositoryMock = new Mock<IDatabaseRepository>();
            var container = new WindsorContainer();

            container.Kernel.ComponentModelBuilder.AddContributor(new SingletonEqualizer());
            container.Install(new ConfigurationInstaller());

            var pushTokens = new List<PushTokenItem> { new PushTokenItem { Token = "https://android.googleapis.com/gcm/send/dCq5No2sfoI:APA91bHiWc9EQ1hSR1lmiAWJVJysgF8-9BAC6lQb20OwusXTVNblCX1J9YejjUgQdEB_QMm_4YwJQ05OsQhOF7yXs8dUmbAxSzdCnSP6YkGFRxfbM0Iu-1V20aXsZJ9uPS00jnZ6MQgH", UserId ="" } };
            databaseRepositoryMock.Setup(x => x.GetPushTokens()).ReturnsAsync(pushTokens);

            var sut = new PushTokenService(container.Resolve<IConfigurationService>(), databaseRepositoryMock.Object);
            await sut.SendPushNotification();


        }
    }
}