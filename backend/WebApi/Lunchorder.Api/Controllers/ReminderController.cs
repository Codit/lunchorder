using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Requests;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("reminders")]
    public class ReminderController : BaseApiController
    {
        private readonly IReminderControllerService _reminderControllerService;

        public ReminderController(IReminderControllerService reminderControllerService)
        {
            if (reminderControllerService == null) throw new ArgumentNullException(nameof(reminderControllerService));
            _reminderControllerService = reminderControllerService;
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

            await _reminderControllerService.Register(token, claimsIdentity);
            return Ok();
        }

        /// <summary>
        /// Update / insert reminder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> SetReminder(PostReminderRequest request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            await _reminderControllerService.SetReminder(request.Reminder, claimsIdentity);
            return Ok();
        }

        /// <summary>
        /// Disables reminder
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK)]
        [Route("")]
        public async Task<IHttpActionResult> DisableReminders()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            // todo, when more reminders, fine grain functionality
            await _reminderControllerService.DeleteReminder(0, claimsIdentity);
            return Ok();
        }

    }
}