using System.Threading.Tasks;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration
{
    [TestFixture]
    public class AuthenticateUserTest : AuthorizationControllerTestBase
    {
        [Test]
        public async Task Login()
        {
            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotEmpty(token.Token);
        }

        [Test]
        public async Task IncorrectLogin()
        {
            var token = await AuthorizeUser(TestConstants.IncorrectUser1.Username, TestConstants.IncorrectUser1.Password);
            Assert.IsNotEmpty(token.Error);

            Assert.AreEqual("Login failed", token.Error);
            Assert.AreEqual("The user name or password is incorrect.", token.ErrorDescription);
        }
    }
}
