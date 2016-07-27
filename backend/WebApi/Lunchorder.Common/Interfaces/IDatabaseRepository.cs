using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.DocumentDb;
using Badge = Lunchorder.Domain.Dtos.Badge;
using Menu = Lunchorder.Domain.Dtos.Menu;
using UserOrderHistory = Lunchorder.Domain.Dtos.UserOrderHistory;

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
    }
}