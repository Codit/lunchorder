using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.ControllerServices
{
    public class BalanceControllerService : IBalanceControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public BalanceControllerService(IDatabaseRepository databaseRepository)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            _databaseRepository = databaseRepository;
        }

        public async Task<decimal> UpdateBalance(string userId, decimal amount, SimpleUser originator)
        {
            var updatedAmount = await _databaseRepository.UpdateBalance(userId, amount, originator);
            return updatedAmount;
        }

        public async Task<UserBalanceAudit> GetUserBalanceHistory(string userId)
        {
            var balanceHistory = await _databaseRepository.GetUserBalanceAndHistory(userId);
            return balanceHistory;
        }
    }
}
