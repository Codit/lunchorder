using System;
using System.Web;
using System.Web.Http;
using Swashbuckle.Application;

namespace Lunchorder.Api
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            httpConfiguration
                .EnableSwagger(c =>
                {
                    c.RootUrl(
                        req =>
                            req.RequestUri.GetLeftPart(UriPartial.Authority) +
                            VirtualPathUtility.ToAbsolute("~/").TrimEnd('/'));
                    c.SingleApiVersion("v1", "Lunchorder.Api");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi(c =>
                    {

                    });
        }

        /// <summary>
		/// Retrieves the path for the XML documentation file
		/// </summary>
		/// <returns>The path of the XML documentation file</returns>
		protected static string GetXmlCommentsPath()
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Lunchorder.Api.XML";
        }
    }
}
