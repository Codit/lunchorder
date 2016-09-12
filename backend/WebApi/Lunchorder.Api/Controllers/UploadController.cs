using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Controllers
{
    [RoutePrefix("uploads")]
    public class UploadController : BaseApiController
    {
        private readonly IUploadControllerService _uploadControllerService;

        public UploadController(IUploadControllerService uploadControllerService)
        {
            _uploadControllerService = uploadControllerService;
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await _uploadControllerService.UploadImage(provider.Contents);
                //// Read the form data.
                //await Request.Content.ReadAsMultipartAsync(provider);

                //// This illustrates how to get the file names.
                //foreach (MultipartFileData file in provider.FileData)
                //{
                //    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                //    Trace.WriteLine("Server file path: " + file.LocalFileName);
                //}
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}