using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// Retrieves order history for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        /// Adds a new order for a user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post()
        {
            return Ok();
        }

        /// <summary>
        /// Removes an order for a user (cancels an order)
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete()
        {
            return Ok();
        }
    }
}