using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
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
        /// Retrieves current open orders for a vendor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vendors")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Domain.Dtos.VendorOrderHistory>))]
        public async Task<IHttpActionResult> GetVendorOrderHistoryForToday()
        {
            var history = await _orderControllerService.GetVendorHistory(DateTime.UtcNow);
            return Ok(history);
        }

        /// <summary>
        /// Retrieves todays order in email format
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vendors/emails")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IHttpActionResult> GetVendorEmailFormat()
        {
            var history = await _orderControllerService.GetEmailVendorHistory(DateTime.UtcNow);
            return Ok(history);
        }

        /// <summary>
        /// Send todays order in email format to vendor
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.PrepayAdmin)]
        [Route("vendors/emails")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IHttpActionResult> SendVendorEmailFormat()
        {
            // todo add apikey for security.
            await _orderControllerService.SendEmailVendorHistory(DateTime.UtcNow);
            return Ok();
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