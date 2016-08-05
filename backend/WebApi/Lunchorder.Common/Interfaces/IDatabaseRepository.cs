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
        Task AddOrder(string userId, string userName, string vendorId, string formattedOrderDate, UserOrderHistory menuOrders);
        Task<VendorOrderHistory> GetVendorOrder(string orderDate, string vendorId);
        Task<decimal> UpdateBalance(string userId, decimal amount, SimpleUser originator);
        Task AddToUserList(string userId, string userName, string firstName, string lastName);
        Task<IEnumerable<PlatformUserListItem>> GetUsers();
    }
}