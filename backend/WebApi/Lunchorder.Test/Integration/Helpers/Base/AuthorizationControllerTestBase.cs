using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Lunchorder.Domain.Constants;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Lunchorder.Test.Integration.Helpers.Base
{
    public class AuthorizationControllerTestBase : ControllerTestBase
    {
        public async Task<TokenResponse> AuthorizeUser(string username, string password)
        {
            var httpContent = AuthorizeHttpContentRequest(username, password);
            var responseResult = AuthorizeUserRequest(httpContent);
            string authorizationContent = await responseResult.Content.ReadAsStringAsync();

            dynamic d = JObject.Parse(authorizationContent);
            var tokenResponse = new TokenResponse();

            var tokenObject = d[ApplicationConstants.AuthTokenIdentifier];

            if (tokenObject != null)
            {
                Token = d[ApplicationConstants.AuthTokenIdentifier].Value as string;
            }

            tokenResponse.Error = d.error;
            tokenResponse.ErrorDescription = d.error_description;
            tokenResponse.Token = Token;

            return tokenResponse;
        }

        protected virtual async Task<HttpResponseMessage> GetAuthorizeAsync(string routeUrl)
        {
            return await GetAuthorizedPayloadRequest(routeUrl).GetAsync();
        }


        protected virtual async Task<HttpResponseMessage> GetUnauthorizeAsync(string routeUrl, Dictionary<string, string> headers)
        {
            return await GetUnauthorizedRequest(routeUrl, headers).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> GetUnauthorizeAsync(string routeUrl)
        {
            return await GetUnauthorizedRequest(routeUrl, null).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostUnauthorizeAsync<TModel>(TModel model, string routeUrl)
        {
            return await GetUnauthorizedPayloadRequest(routeUrl, null, model).PostAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostUnauthorizeAsync<TModel>(TModel model, string routeUrl, Dictionary<string, string> headers)
        {
            return await GetUnauthorizedPayloadRequest(routeUrl, headers, model).PostAsync();
        }

        protected virtual async Task<HttpResponseMessage> PutAuthorizeAsync<TModel>(TModel model, string routeUrl)
        {
            return await CustomVerbAuthorizeRequestAsync(model, HttpMethod.Put, routeUrl);
        }

        protected virtual async Task<HttpResponseMessage> DeleteAuthorizeAsync(string routeUrl)
        {
            return await CustomVerbAuthorizeRequestAsync(HttpMethod.Delete, routeUrl);
        }

        protected virtual async Task<HttpResponseMessage> PostAuthorizedAsync<TModel>(TModel model, string routeUri)
        {
            return await PostAuthorizedAsync(model, routeUri, null);
        }

        protected virtual async Task<HttpResponseMessage> PostAuthorizedAsync<TModel>(TModel model, string routeUri, Dictionary<string, string> headers)
        {
            return await GetAuthorizedPayloadRequest(routeUri, model, headers).PostAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAuthorizedAsync(string appRouteUri)
        {
            return await GetAuthorizedRequest(appRouteUri).PostAsync();
        }


        protected virtual async Task<HttpResponseMessage> PostUnauthorizedAppAsync<TModel>(TModel model, string appRouteUri, Dictionary<string, string> headers)
        {
            return await PostUnauthorizedAsync(model, headers, appRouteUri);
        }

        protected virtual async Task<HttpResponseMessage> PostAuthorizeAsync<TModel>(TModel model, string portalRouteUri, Dictionary<string, string> headers)
        {
            return await PostAuthorizedAsync(model, portalRouteUri, headers);
        }

        protected virtual async Task<HttpResponseMessage> PostAuthorizeAsync<TModel>(TModel model, string portalRouteUri)
        {
            return await PostAuthorizedAsync(model, portalRouteUri);
        }

        private RequestBuilder GetAuthorizedRequest(string uri)
        {
            Assert.IsNotNull(Token, "There was no token retrieved from an authorization request");
            Assert.IsNotEmpty(Token, "There was no token retrieved from an authorization request");
            return Server.CreateRequest(uri).AddHeader("Authorization", "Bearer " + Token);
        }

        private RequestBuilder GetUnauthorizedRequest(string uri, Dictionary<string, string> headers)
        {
            var requestBuilder = Server.CreateRequest(uri);
            addHeaders(requestBuilder, headers);
            return requestBuilder;
        }

        private void addHeaders(RequestBuilder requestBuilder, Dictionary<string, string> headers)
        {
            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    requestBuilder.AddHeader(header.Key, header.Value);
                }
            }
        }

        private RequestBuilder GetAuthorizedPayloadRequest<TModel>(string uri, TModel model, Dictionary<string, string> headers)
        {
            var requestBuilder = GetAuthorizedRequest(uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()));
            addHeaders(requestBuilder, headers);
            return requestBuilder;
        }

        private RequestBuilder GetAuthorizedPayloadRequest(string uri)
        {
            return GetAuthorizedRequest(uri)
                .And(request => request.Content = new StringContent(""));
        }

        private RequestBuilder GetUnauthorizedPayloadRequest<TModel>(string uri, Dictionary<string, string> headers, TModel model)
        {
            return GetUnauthorizedRequest(uri, headers)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()));
        }

        private RequestBuilder GetUnauthorizedPayloadRequest<TModel>(string uri, TModel model)
        {
            return GetUnauthorizedRequest(uri, null)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()));
        }

        protected async Task<HttpResponseMessage> PostUnauthorizedAsync<TModel>(TModel model, Dictionary<string, string> headers, string uri)
        {
            return await GetUnauthorizedPayloadRequest(uri, headers, model)
                .PostAsync();
        }

        protected async Task<HttpResponseMessage> PostUnauthorizedAsync<TModel>(TModel model, string uri)
        {
            return await GetUnauthorizedPayloadRequest(uri, model)
                .PostAsync();
        }

        protected async Task<HttpResponseMessage> PutAuthorizedAsync<TModel>(TModel model, string uri)
        {
            return await CustomVerbAuthorizeRequestAsync(model, HttpMethod.Put, uri);
        }

        protected async Task<HttpResponseMessage> PutUnauthorizedAsync<TModel>(TModel model, string uri)
        {
            return await CustomVerbUnauthorizeRequestAsync(model, HttpMethod.Put, uri);
        }

        private async Task<HttpResponseMessage> CustomVerbAuthorizeRequestAsync(HttpMethod method, string uri)
        {
            return await GetAuthorizedRequest(uri)
                .SendAsync(method.ToString());
        }

        private async Task<HttpResponseMessage> CustomVerbAuthorizeRequestAsync<TModel>(TModel model, HttpMethod method, string uri)
        {
            return await GetAuthorizedRequest(uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .SendAsync(method.ToString());
        }

        private async Task<HttpResponseMessage> CustomVerbUnauthorizeRequestAsync<TModel>(TModel model, HttpMethod method, string uri)
        {
            return await GetUnauthorizedPayloadRequest(uri, model)
                .SendAsync(method.ToString());
        }
    }
}
