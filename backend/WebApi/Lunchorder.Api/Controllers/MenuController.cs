using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Requests;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("menus")]
    public class MenuController : BaseApiController
    {
        private readonly IMenuControllerService _menuControllerService;

        public MenuController(IMenuControllerService menuControllerService)
        {
            if (menuControllerService == null) throw new ArgumentNullException(nameof(menuControllerService));
            _menuControllerService = menuControllerService;
        }

        /// <summary>
        /// returns the active lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Domain.Dtos.Menu))]
        public async Task<IHttpActionResult> Get()
        {
            var menu = await _menuControllerService.GetActiveMenu();
            return Ok(menu);
        }

        /// <summary>
        /// Adds a new lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Post(PostMenuRequest postMenuRequest)
        {
            await _menuControllerService.Add(postMenuRequest.Menu);
            return Ok();
        }

        /// <summary>
        /// Updates an existing lunch menu
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Domain.Dtos.Menu))]
        public async Task<IHttpActionResult> Put(PutMenuRequest putMenuRequest)
        {
            await _menuControllerService.Update(putMenuRequest.Menu);
            return Ok();
        }


        /// <summary>
        /// Updates active menu
        /// </summary>
        /// <returns></returns>
        [Route("active/{menuId}")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Domain.Dtos.Menu))]
        public async Task<IHttpActionResult> SetActive(string menuId)
        {
            // todo, only authorize admin
            await _menuControllerService.SetActive(menuId);
            return Ok();
        }

        /// <summary>
        /// Sets a lunch menu to disabled
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Delete(string menuId)
        {
            await _menuControllerService.Delete(menuId);
            return Ok();
        }
    }
}