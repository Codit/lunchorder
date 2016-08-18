using System;
using System.Collections.Generic;
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
    public class OrderControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "orders";

        [Test]
        public async Task Post()
        {
            var menuOrders = new List<MenuOrder>();

            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await PostAuthorizeAsync(new PostOrderRequest { MenuOrders = menuOrders }, string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedOrderControllerService.Verify(x => x.Add(TestConstants.User1.Id, TestConstants.User1.UserName, TestConstants.User1.FullName, menuOrders), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task SendVendorEmailFormat()
        {
            var token = await AuthorizeUser(TestConstants.User4.UserName, TestConstants.User4.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await PostAuthorizeAsync(new { }, $"{_routePrefix}/vendors/emails");

            MockedApiInstaller.MockedOrderControllerService.Verify(x => x.SendEmailVendorHistory(It.IsAny<DateTime>()), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task SendVendorEmailFormat_Unauthorized()
        {
            var token = await AuthorizeUser(TestConstants.User1.UserName, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await PostAuthorizeAsync(new { }, $"{_routePrefix}/vendors/emails");

            MockedApiInstaller.MockedOrderControllerService.Verify(x => x.SendEmailVendorHistory(It.IsAny<DateTime>()), Times.Never);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}