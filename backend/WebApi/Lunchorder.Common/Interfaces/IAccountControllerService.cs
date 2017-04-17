using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IAccountControllerService
    {
        Task<GetUserInfoResponse> GetUserInfo(ClaimsIdentity claimsIdentity);
        Task<GetAllUsersResponse> GetAllUsers();
        Task<IEnumerable<LastOrder>> GetLast5Orders(ClaimsIdentity claimsIdentity);
        Task PromoteUserToPrepay(string userid);
    }
}