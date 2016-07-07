using System.Web.Http;

namespace Lunchorder.Api.Controllers
{
	[RoutePrefix("lunchorder")]
	public class LunchOrderController : BaseApiController
	{
		[Route("")]
		// GET api/<controller>
		public IHttpActionResult Get()
		{
			return Ok();
		}
	}
}