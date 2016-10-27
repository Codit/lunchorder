using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("pushes")]
    public class PushController : BaseApiController
    {
        private readonly IPushControllerService _pushControllerService;

        public PushController(IPushControllerService pushControllerService)
        {
            if (pushControllerService == null) throw new ArgumentNullException(nameof(pushControllerService));
            _pushControllerService = pushControllerService;
        }

        /// <summary>
        /// Registers a client
        /// </summary>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Register(string token)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            await _pushControllerService.Register(token, claimsIdentity);
            return Ok();
        }

    }
}