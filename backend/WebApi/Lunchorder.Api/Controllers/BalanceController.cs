using System;
using System.Net;
using Lunchorder.Common.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
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
            if (balanceControllerService == null) throw new ArgumentNullException(nameof(balanceControllerService));
            _balanceControllerService = balanceControllerService;
        }

        /// <summary>
        /// Updates the balance for a user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Roles.PrepayAdmin)]
        [HttpPut]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(decimal))]
        public async Task<IHttpActionResult> Put(PutBalanceRequest putBalanceRequest)
        {
            var originator = new SimpleUser { Id = User.Identity.GetUserId(), UserName = User.Identity.GetUserName()};
            var result = await _balanceControllerService.UpdateBalance(putBalanceRequest.UserId, putBalanceRequest.Amount, originator);

            // todo location uri
            return Created("", result);
        }
    }
}