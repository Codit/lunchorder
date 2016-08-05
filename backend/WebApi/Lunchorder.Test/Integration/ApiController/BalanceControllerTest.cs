using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Requests;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.ApiController
{
    [TestFixture]
    public class BalanceControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "balances";

        [Test]
        public async Task Put()
        {
            var originator = new SimpleUser {Id = TestConstants.User4.Id, UserName = TestConstants.User4.UserName};
            var amount = 4.4M;
            MockedApiInstaller.MockedBalanceControllerService.Setup(
               x => x.UpdateBalance(TestConstants.User1.Id, amount, originator))
               .ReturnsAsync(new decimal());

            var token = await AuthorizeUser(TestConstants.User4.UserName, TestConstants.User4.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await PutAuthorizeAsync(new PutBalanceRequest { UserId = TestConstants.User1.Id, Amount = amount }, string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedBalanceControllerService.Verify(x => x.UpdateBalance(TestConstants.User1.Id, amount, originator), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// User should have specified role
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Put_Unauthorized()
        {
            var amount = 4.4M;
            MockedApiInstaller.MockedBalanceControllerService.Setup(
               x => x.UpdateBalance(TestConstants.User1.Id, amount, new SimpleUser()))
               .ReturnsAsync(new decimal());

            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await PutAuthorizeAsync(new PutBalanceRequest { UserId = TestConstants.User1.Id, Amount = amount }, string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedBalanceControllerService.Verify(x => x.UpdateBalance(TestConstants.User1.Id, amount, new SimpleUser()), Times.Never);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}