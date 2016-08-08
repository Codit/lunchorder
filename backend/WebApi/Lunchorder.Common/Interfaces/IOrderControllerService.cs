using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IOrderControllerService
    {
        Task<IEnumerable<UserOrderHistory>> GetHistory(string userId);
        Task Delete(Guid orderId);
        Task Add(string userId, string userName, IEnumerable<MenuOrder> menuOrders);
        Task<VendorOrderHistory> GetVendorHistory(DateTime utcNow);
    }
}