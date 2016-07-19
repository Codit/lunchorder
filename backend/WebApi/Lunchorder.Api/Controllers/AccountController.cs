using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetUserInfoResponse))]
        public async Task<IHttpActionResult> Get(string userId)
        {
            // todo extract user
            return Ok(_accountControllerService.GetUserInfo(userId));
        }
    }
}