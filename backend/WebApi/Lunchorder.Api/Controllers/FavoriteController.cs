using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("favorites")]
    public class FavoriteController : BaseApiController
    {
        /// <summary>
        /// Gets the favorites for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        /// Adds a new favorite for a user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post()
        {
            return Ok();
        }

        /// <summary>
        /// Deletes a favorite for a user
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