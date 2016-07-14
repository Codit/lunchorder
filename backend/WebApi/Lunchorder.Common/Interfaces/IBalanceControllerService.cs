using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IBalanceControllerService
    {
        Task<double> UpdateBalance(string userId, double amount);
    }
}