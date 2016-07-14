using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using IoT.Portal.Api.Common.Extensions;
using IoT.Portal.Api.Common.Interfaces;

namespace IoT.Portal.Api.Infrastructure.Filters
{
    /// <summary>
    /// Marker attribute in order to enable IoC, .Net uses the <see cref="AuthorizeEndpointFilter"/> after compilation
    /// </summary>
    public class AuthorizeEndpointFilterAttribute : Attribute { }

    /// <summary>
    /// Action filter that checks the request for a valid authorization endpoint.
    /// </summary>
    public class AuthorizeEndpointFilter : AuthorizeAttribute
    {
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeEndpointFilter"/> class.
        /// </summary>
        /// <param name="configurationService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AuthorizeEndpointFilter(IConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            _configurationService = configurationService;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // todo unit test!
            if (actionContext.ActionDescriptor.GetCustomAttributes<AuthorizeEndpointFilterAttribute>().Any())
            {
                if (string.IsNullOrEmpty(_configurationService.AuthenticationEndpoint))
                    return;

                var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
                if (claimsIdentity == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var isGrantedAuthorization = claimsIdentity.Claims.IsGrantedAuthorization(_configurationService.AuthenticationEndpoint, _configurationService.Clients);

                if (!isGrantedAuthorization)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception($"No access to {_configurationService.AuthenticationEndpoint}, either the endpoint does not belong to the user's claim or he does not have a matching client subscription."));
                    throw new UnauthorizedAccessException();
                }
            }
        }

        protected override bool IsAuthorized(HttpActionContext context)
        {
            var client = Thread.CurrentPrincipal;
            return client.Identity.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            throw new UnauthorizedAccessException();
        }
    }
}