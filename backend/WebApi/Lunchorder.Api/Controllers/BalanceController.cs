using System;
using System.Net;
using System.Security.Claims;
using Lunchorder.Common.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Api.Infrastructure.Filters;
using Lunchorder.Common.Extensions;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Requests;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("balances")]
    public class BalanceController : BaseApiController
    {
        private readonly IBalanceControllerService _balanceControllerService;

        public BalanceController(IBalanceControllerService balanceControllerService)
        {
            _balanceControllerService = balanceControllerService ?? throw new ArgumentNullException(nameof(balanceControllerService));
        }

        /// <summary>
        /// Updates the balance for a user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Roles.PrepayAdmin)]
        [HttpPut]
        [Route("")]
        [ValidateModelStateFilter]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(decimal))]
        public async Task<IHttpActionResult> Put(PutBalanceRequest putBalanceRequest)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            var fullName = claimsIdentity.GetFullNameFromClaims();
            var originator = new SimpleUser { Id = User.Identity.GetUserId(), UserName = User.Identity.GetUserName(), FullName = fullName };
            var result = await _balanceControllerService.UpdateBalance(putBalanceRequest.UserId, putBalanceRequest.Amount, originator);

            // todo location uri
            return Created("", result);
        }

        /// <summary>
        /// Updates the balance for a user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Roles.PrepayAdmin)]
        [HttpGet]
        [Route("histories")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserBalanceAudit))]
        public async Task<IHttpActionResult> GetUserBalanceHistory(string userId)
        {
            var result = await _balanceControllerService.GetUserBalanceHistory(userId);
            return Ok(result);
        }
    }
}