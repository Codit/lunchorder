using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IBalanceControllerService
    {
        Task<decimal> UpdateBalance(string userId, decimal amount, SimpleUser originator);
        Task<UserBalanceAudit> GetUserBalanceHistory(string userId);
    }
}