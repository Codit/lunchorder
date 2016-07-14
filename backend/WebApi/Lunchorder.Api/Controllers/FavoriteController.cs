using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Requests;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("favorites")]
    public class FavoriteController : BaseApiController
    {
        private readonly IFavoriteControllerService _favoriteControllerService;

        public FavoriteController(IFavoriteControllerService favoriteControllerService)
        {
            if (favoriteControllerService == null) throw new ArgumentNullException(nameof(favoriteControllerService));
            _favoriteControllerService = favoriteControllerService;
        }

        /// <summary>
        /// Gets the favorites for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Dtos.MenuEntryFavorite>))]
        public async Task<IHttpActionResult> Get()
        {
            // todo extract user id
            var favorites = await _favoriteControllerService.Get("");
            return Ok(favorites);
        }

        /// <summary>
        /// Adds a new favorite for a user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Post(PostFavoriteRequest postFavoriteRequest)
        {
            // todo extract user id
            await _favoriteControllerService.Add("", postFavoriteRequest.MenuEntryId);
            return Ok();
        }

        /// <summary>
        /// Deletes a favorite for a user
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Delete(Guid favoriteId)
        {
            // todo extract user id
            await _favoriteControllerService.Delete("", favoriteId);
            return Ok();
        }
    }
}