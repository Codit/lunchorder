using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("balances")]
    public class BalanceController : BaseApiController
    {
        // todo, only access for administrator
        /// <summary>
        /// Updates the balance for a user
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put()
        {
            return Ok();
        }
    }
}