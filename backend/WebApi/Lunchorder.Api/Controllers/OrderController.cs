using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Requests;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [Authorize]
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderControllerService _orderControllerService;

        public OrderController(IOrderControllerService orderControllerService)
        {
            if (orderControllerService == null) throw new ArgumentNullException(nameof(orderControllerService));
            _orderControllerService = orderControllerService;
        }

        /// <summary>
        /// Retrieves order history for a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Dtos.UserOrderHistory>))]
        public async Task<IHttpActionResult> Get()
        {
            // todo extract user id
            var history = await _orderControllerService.GetHistory("");
            return Ok(history);
        }

        /// <summary>
        /// Adds a new order for a user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Post(PostOrderRequest postOrderRequest)
        {
            await _orderControllerService.Add(User.Identity.GetUserId(), User.Identity.GetUserName(), postOrderRequest.MenuOrders);
            return Ok();
        }

        /// <summary>
        /// Removes an order for a user (cancels an order)
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Delete(Guid orderId)
        {
            await _orderControllerService.Delete(orderId);
            return Ok();
        }
    }
}