using System.Threading.Tasks;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.ApiController
{
    [TestFixture]
    public class LunchorderControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "lunchorder";

        [Test]
        public async Task Get()
        {
            
            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }
    }
}
