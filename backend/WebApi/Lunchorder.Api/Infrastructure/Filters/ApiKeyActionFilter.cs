using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IoT.Portal.Api.Common.Interfaces;
using IoT.Portal.Api.Domain.Constants;

namespace IoT.Portal.Api.Infrastructure.Filters
{
    /// <summary>
    /// Marker attribute in order to enable IoC, .Net uses the <see cref="ApiKeyActionFilter"/> after compilation
    /// </summary>
    public class ApiKeyActionFilterAttribute : Attribute
    {

    }

    /// <summary>
    /// Action filter that checks the request for a valid api key.
    /// </summary>
    public class ApiKeyActionFilter : ActionFilterAttribute
    {
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyActionFilter"/> class.
        /// </summary>
        /// <param name="configurationService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApiKeyActionFilter(IConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            _configurationService = configurationService;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<ApiKeyActionFilterAttribute>().Any())
            {
                var request = actionContext.Request;

                IEnumerable<string> apiKeyHeaderValues;
                if (request.Headers.TryGetValues(RequestHeaderConstants.ApiKeyHeader, out apiKeyHeaderValues))
                {
                    var apiKeyHeaderValue = apiKeyHeaderValues.First();
                    var keyLookup = _configurationService.ApiKeys.FirstOrDefault(x => x.Value == apiKeyHeaderValue);

                    if (keyLookup == null)
                    {
                        throw new UnauthorizedAccessException();

                    }
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }
    }
}