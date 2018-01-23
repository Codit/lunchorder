using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IBadgeControllerService
    {
        Task<GetBadgesResponse> Get();
    }
}