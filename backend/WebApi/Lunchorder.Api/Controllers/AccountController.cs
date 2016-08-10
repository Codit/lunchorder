using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
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

            return Ok(await _accountControllerService.GetUserInfo(claimsIdentity));
        }

        /// <summary>
        /// Gets the last 5 orders for a user
        /// </summary>
        /// <returns></returns>
        [Route("last5Orders")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<LastOrder>))]
        public async Task<IHttpActionResult> GetLast5Orders()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            return Ok(await _accountControllerService.GetLast5Orders(claimsIdentity));
        }

        /// <summary>
        /// Gets all the users of the platform
        /// </summary>
        /// <returns></returns>
        [Route("users")]
        [HttpGet]
        [Authorize(Roles = Roles.PrepayAdmin)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetAllUsersResponse))]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            return Ok(await _accountControllerService.GetAllUsers());
        }
    }
}