using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IBadgeControllerService
    {
        Task<GetBadgesResponse> Get();
        Task<IEnumerable<string>> SetOrderBadges(string username, string userId);
        Task<IEnumerable<string>> SetPrepayBadges(string userId);
    }
}