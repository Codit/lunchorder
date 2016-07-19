using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IAccountControllerService
    {
        Task<GetUserInfoResponse> GetUserInfo(string userId, string username, string email);
    }
}