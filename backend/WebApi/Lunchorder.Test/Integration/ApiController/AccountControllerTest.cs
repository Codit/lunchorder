using System.Security.Claims;
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
                x => x.GetUserInfo(new ClaimsIdentity()))
                .ReturnsAsync(new GetUserInfoResponse());
            
            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedAccountControllerService.Verify(x => x.GetUserInfo(new ClaimsIdentity()), Times.Once);
            
            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAllUsers()
        {
            MockedApiInstaller.MockedAccountControllerService.Setup(
                x => x.GetAllUsers())
                .ReturnsAsync(new GetAllUsersResponse());

            var token = await AuthorizeUser(TestConstants.User4.UserName, TestConstants.User4.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}/users"));

            MockedApiInstaller.MockedAccountControllerService.Verify(x => x.GetAllUsers(), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// user is not part of specified role
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllUsers_Unauthorized()
        {
            MockedApiInstaller.MockedAccountControllerService.Setup(
                x => x.GetAllUsers())
                .ReturnsAsync(new GetAllUsersResponse());

            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}/users"));

            MockedApiInstaller.MockedAccountControllerService.Verify(x => x.GetAllUsers(), Times.Never);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}