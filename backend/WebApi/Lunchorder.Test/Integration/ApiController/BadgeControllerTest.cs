﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Test.Integration.Helpers;
using Lunchorder.Test.Integration.Helpers.Base;
using Moq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.ApiController
{
    [TestFixture]
    public class BadgeControllerTest : AuthorizationControllerTestBase
    {
        private readonly string _routePrefix = "badges";

        [Test]
        public async Task Get()
        {
            MockedApiInstaller.MockedBadgeControllerService.Setup(x => x.Get()).ReturnsAsync(new List<Badge>());
            
            var token = await AuthorizeUser(TestConstants.User1.Username, TestConstants.User1.Password);
            Assert.IsNotNullOrEmpty(token.Token);

            var response = await GetAuthorizeAsync(string.Format($"{_routePrefix}"));

            MockedApiInstaller.MockedBadgeControllerService.Verify(x => x.Get(), Times.Once);
            
            AssertAndLogInvalidModelState(response, System.Net.HttpStatusCode.OK);
        }
    }
}