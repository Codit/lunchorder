using System;
using System.Net;
using Lunchorder.Common.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
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

        // todo, only access for administrator
        /// <summary>
        /// Updates the balance for a user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(double))]
        public async Task<IHttpActionResult> Put(double amount)
        {
            // todo add user
            var result = await _balanceControllerService.UpdateBalance("", amount);

            // todo location uri
            return Created("", result);
        }
    }
}