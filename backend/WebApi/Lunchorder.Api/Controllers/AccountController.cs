using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountControllerService _accountControllerService;

        public AccountController(IAccountControllerService accountControllerService)
        {
            if (accountControllerService == null) throw new ArgumentNullException(nameof(accountControllerService));
            _accountControllerService = accountControllerService;
        }

        /// <summary>
        /// Gets the info for a user
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetUserInfoResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            
            if (claimsIdentity == null)
                return InternalServerError();

            bool isAzureActiveDirectoryUser = false;
            var hasIssClaim = claimsIdentity.FindFirst("iss");
            if (hasIssClaim != null)
            {
                if (hasIssClaim.Value.Contains("sts.windows.net"))
                {
                    isAzureActiveDirectoryUser = true;
                }
            }

            return Ok(await _accountControllerService.GetUserInfo(User.Identity.GetUserName(), isAzureActiveDirectoryUser));
        }
    }
}