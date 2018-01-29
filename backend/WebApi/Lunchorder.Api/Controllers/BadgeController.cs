using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos.Requests;
using Lunchorder.Domain.Dtos.Responses;
using Microsoft.AspNet.Identity;
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

        /// <summary>
        /// Persist badges and returns a list of earned badges for the last order
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("order")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<string>))]
        public async Task<IHttpActionResult> SetOrderBadges()
        {
            if (!(User.Identity is ClaimsIdentity claimsIdentity))
                return InternalServerError();

            var result = await _badgeControllerService.SetOrderBadges(claimsIdentity.GetUserName(), claimsIdentity.GetUserId());
            return Ok(result);
        }

        /// <summary>
        /// Persist badges and returns a list of earned badges for the last order
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Roles.PrepayAdmin)]
        [HttpPost]
        [Route("prepay")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<string>))]
        public async Task<IHttpActionResult> SetPrepayBadges(SetPrepayBadgesRequest request)
        {
            var result = await _badgeControllerService.SetPrepayBadges(request.Username);
            return Ok(result);
        }
    }
}