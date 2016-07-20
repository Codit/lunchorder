using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.ApiController
{
    [TestFixture]
    public class AccountControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "accounts";

        [Test]
        public async Task Get()
        {
            MockedApiInstaller.MockedAccountControllerService.Setup(
                x => x.GetUserInfo(TestConstants.User1.Username, false))
                .ReturnsAsync(new GetUserInfoResponse());
            
            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedAccountControllerService.Verify(x => x.GetUserInfo( TestConstants.User1.Username, false), Times.Once);
            
            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }
    }
}