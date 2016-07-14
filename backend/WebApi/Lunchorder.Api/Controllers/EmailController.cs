using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("emails")]
    public class EmailController : BaseApiController
    {
        private readonly IEmailControllerService _emailControllerService;

        public EmailController(IEmailControllerService emailControllerService)
        {
            if (emailControllerService == null) throw new ArgumentNullException(nameof(emailControllerService));
            _emailControllerService = emailControllerService;
        }

        // todo add api key to trigger this operation.
        /// <summary>
        /// When triggered, it should send an email
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IHttpActionResult> Post()
        {
            return Ok(await _emailControllerService.SendEmail());
        }
    }
}