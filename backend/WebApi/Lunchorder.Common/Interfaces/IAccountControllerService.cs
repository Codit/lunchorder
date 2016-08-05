using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IAccountControllerService
    {
        Task<GetUserInfoResponse> GetUserInfo(ClaimsIdentity claimsIdentity);
        Task<GetAllUsersResponse> GetAllUsers();
    }
}