using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Test.Integration.Helpers.Base.IoC;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class ControllerTestBase
    {
        protected TestServer Server;
        protected string Token;

        /// <summary>
        /// Runs before each tests is launched
        /// </summary>
        [SetUp]
        public virtual void BeforeEachTest()
        {
            var startup = new TestStartup();
            Server = TestServer.Create(builder => startup.Configuration(builder));
            MockedApiInstaller = startup.MockedApiInstaller;

            var documentDbBase = new DocumentDbBase(startup.Container.Resolve<IDocumentStore>());
            documentDbBase.Init();
        }

        public MockedApiControllerInstaller MockedApiInstaller { get; set; }

        /// <summary>
        /// Runs after each test has been executed.
        /// </summary>
        [TearDown]
        public virtual void AfterEachTest()
        {
            Server?.Dispose();
            Token = null;
        }

        /// <summary>
        /// Logs the invalid model state for a given <see cref="HttpStatusCode"/>
        /// </summary>
        public void AssertAndLogInvalidModelState(HttpResponseMessage response, HttpStatusCode expected)
        {
            var responseResult = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(expected, response.StatusCode, responseResult);
        }

        /// <summary>
        /// Logs the invalid model state when status is not <see cref="HttpStatusCode.OK"/>
        /// </summary>
        public void AssertAndLogInvalidModelState(HttpResponseMessage response)
        {
            AssertAndLogInvalidModelState(response, HttpStatusCode.OK);
        }

        protected HttpContent RefreshTokenHttpContentRequest(string clientId, string refreshToken)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
            });
            return content;
        }

        protected HttpContent AuthorizeHttpContentRequest(string username, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            return content;
        }

        protected HttpContent AuthorizeHttpContentRequest(string username, string password, string clientId)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            return content;
        }

        public HttpResponseMessage AuthorizeUserRequest(HttpContent httpContent)
        {
            try
            {
                var url = GetAuthenticationUrl();
                var result = Server.HttpClient.PostAsync(url, httpContent).Result;

                return result;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        protected string GetAuthenticationUrl()
        {
            return "/oauth/token";
        }
    }
}
