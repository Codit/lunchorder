using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("badges")]
    public class BadgeController : BaseApiController
    {
        private readonly IBadgeControllerService _badgeControllerService;

        public BadgeController(IBadgeControllerService badgeControllerService)
        {
            if (badgeControllerService == null) throw new ArgumentNullException(nameof(badgeControllerService));
            _badgeControllerService = badgeControllerService;
        }

        /// <summary>
        /// Gets all platform badges
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Badge>))]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _badgeControllerService.Get();
            return Ok(result);
        }
    }
}