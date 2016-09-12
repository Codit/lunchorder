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
        public async Task CreateWithoutPassword()
        {
            var username = "username 123456";
            var email = "user@name.123456";
            var firstName = "first";
            var lastName = "name";
            var dbUserId = await ApplicationUserService.Create(username, email, firstName, lastName);
            Assert.AreNotEqual(new Guid().ToString(), dbUserId);
            Assert.IsNullOrEmpty(dbUserId.PasswordHash);
        }

        [Test]
        public async Task CreateWithPassword()
        {
            for (var i = 0; i < 20; i++)
            {
                await CreateUserWithPassword(i, "demo_admin");
            }

            for (var i = 0; i < 20; i++)
            {
                await CreateUserWithPassword(i, "demo_user");
            }
        }

        private async Task CreateUserWithPassword(int i, string username)
        {
            if (i > 0)
            {
                username += i;
            }

            var email = $"{username}@lunchorder.be";
            var name = username.Split('_');
            var firstName = name[0];
            var lastName = name[1];
            var dbUserId = await ApplicationUserService.Create(username, email, firstName, lastName, password: username);

            Assert.AreNotEqual(new Guid().ToString(), dbUserId);
            Assert.IsNotNullOrEmpty(dbUserId.PasswordHash);
        }
    }
}
