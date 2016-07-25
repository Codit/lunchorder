using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.ApiController
{
    [TestFixture]
    public class MenuControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "menus";

        [Test]
        public async Task Get()
        {
            MockedApiInstaller.MockedMenuControllerService.Setup(x => x.GetActiveMenu()).ReturnsAsync(new Menu());
            
            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedMenuControllerService.Verify(x => x.GetActiveMenu(), Times.Once);
            
            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task Post()
        {
            MockedApiInstaller.MockedMenuControllerService.Setup(x => x.GetActiveMenu()).ReturnsAsync(new Menu());

            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedMenuControllerService.Verify(x => x.GetActiveMenu(), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task Put()
        {
            MockedApiInstaller.MockedMenuControllerService.Setup(x => x.GetActiveMenu()).ReturnsAsync(new Menu());

            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedMenuControllerService.Verify(x => x.GetActiveMenu(), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task SetActive()
        {
            var menuId = "123456";
            MockedApiInstaller.MockedMenuControllerService.Setup(x => x.SetActive(menuId));

            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}/active/{menuId}"));

            MockedApiInstaller.MockedMenuControllerService.Verify(x => x.SetActive(menuId), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task Delete()
        {
            var menuId = "123456";
            MockedApiInstaller.MockedMenuControllerService.Setup(x => x.Delete(menuId));

            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedMenuControllerService.Verify(x => x.GetActiveMenu(), Times.Once);

            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }

    }
}