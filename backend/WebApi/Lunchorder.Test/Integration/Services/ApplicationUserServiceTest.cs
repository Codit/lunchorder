using System;
using System.Threading.Tasks;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Services
{
    [TestFixture]
    public class ApplicationUserServiceTest : ServiceBase
    {
        [Test]
        public async Task Create()
        {
            var username = "username 123456";
            var email = "user@name.123456";
            var firstName = "first";
            var lastName = "name";
            var dbUserId = await ApplicationUserService.Create(username, email, firstName, lastName);
            Assert.AreNotEqual(new Guid().ToString(), dbUserId);
        }
    }
}
