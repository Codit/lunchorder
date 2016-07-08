using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("emails")]
    public class EmailController : BaseApiController
    {
        // todo add api key to trigger this operation.
        /// <summary>
        /// When triggered, it should send an email
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post()
        {
            return Ok();
        }
    }
}