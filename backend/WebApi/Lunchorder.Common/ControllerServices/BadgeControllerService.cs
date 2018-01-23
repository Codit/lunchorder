using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.ControllerServices
{
    public class BadgeControllerService : IBadgeControllerService
    {
        public async Task<GetBadgesResponse> Get()
        {
            var response = new GetBadgesResponse{ Badges = await Task.FromResult(Badges.BadgeList) };
            return response;
        }
    }
}
