using System.Threading.Tasks;
using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("menus")]
    public class MenuController : BaseApiController
    {
        /// <summary>
        /// returns the active lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        /// Adds a new lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            return Ok();
        }

        /// <summary>
        /// Updates an existing lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public IHttpActionResult Put()
        {
            return Ok();
        }

        /// <summary>
        /// Sets a lunch menu to disabled
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete()
        {
            return Ok();
        }
    }
}