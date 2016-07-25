using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<GetUserInfoResponse> GetUserInfo(string userId);
        Task<IEnumerable<Badge>> GetBadges();
        Task AddMenu(Menu menu);
        Task<Menu> GetEnabledMenu();
        Task<Menu> GetMenu(string menuId);
        Task<Menu> UpdateMenu(Menu menu);
        Task SetActiveMenu(string menuId);
        Task DeleteMenu(string menuId);
    }
}