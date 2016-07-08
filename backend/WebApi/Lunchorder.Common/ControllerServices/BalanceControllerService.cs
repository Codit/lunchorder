using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;

namespace Lunchorder.Common.ControllerServices
{
    public class BalanceControllerService : IBalanceControllerService
    {
        public async Task<double> UpdateBalance(string userId, double amount)
        {
            return await Task.FromResult(amount);
        }
    }
}
