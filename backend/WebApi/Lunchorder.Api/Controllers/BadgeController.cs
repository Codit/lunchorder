using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("badges")]
    public class BadgeController : BaseApiController
    {
        private readonly IBadgeControllerService _badgeControllerService;

        public BadgeController(IBadgeControllerService badgeControllerService)
        {
            _badgeControllerService = badgeControllerService ?? throw new ArgumentNullException(nameof(badgeControllerService));
        }

        /// <summary>
        /// Gets all platform badges
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetBadgesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _badgeControllerService.Get();
            return Ok(result);
        }
    }
}