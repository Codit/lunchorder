using System.Threading.Tasks;
using System.Web.Http;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Api.Controllers
{
    /// <summary>
    /// Controller that performs checks and seeds data
    /// </summary>
    [RoutePrefix("checklist")]
    public class ChecklistController : BaseApiController
    {
        private readonly IChecklistControllerService _checklistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistController"/> class.
        /// </summary>
        /// <param name="checklistService">The checklist service</param>
        public ChecklistController(IChecklistControllerService checklistService)
        {
            _checklistService = checklistService;
        }

        [HttpPost]
        [Route("seeduser")]
        public async Task<IHttpActionResult> SeedUserData()
        {
            await _checklistService.SeedUserData();
            return Ok();
        }
    }
}