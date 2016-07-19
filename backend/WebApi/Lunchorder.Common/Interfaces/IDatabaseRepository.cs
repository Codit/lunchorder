using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<GetUserInfoResponse> GetUserInfo(string userId);
    }
}