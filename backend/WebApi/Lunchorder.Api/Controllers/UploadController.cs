using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lunchorder.Common.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("uploads")]
    public class UploadController : BaseApiController
    {
        private readonly IUploadControllerService _uploadControllerService;
        private readonly IUserService _userService;

        public UploadController(IUploadControllerService uploadControllerService, IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException(nameof(userService));
            _uploadControllerService = uploadControllerService;
            _userService = userService;
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> PostFormData()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                return InternalServerError();

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var url = await _uploadControllerService.UploadImage(provider.Contents);
            await _uploadControllerService.UpdateUserImage(claimsIdentity.GetUserId(), url);
            return Ok(url);
        }
    }
}