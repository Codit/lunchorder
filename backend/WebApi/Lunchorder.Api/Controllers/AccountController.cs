using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// Gets the info for a user (balance, user profile, badgets, favorites, ...)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }
    }
}