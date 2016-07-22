using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.ControllerServices
{
    public class BadgeControllerService : IBadgeControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public BadgeControllerService(IDatabaseRepository databaseRepository)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            _databaseRepository = databaseRepository;
        }

        public async Task<IEnumerable<Badge>> Get()
        {
            var badges = await _databaseRepository.GetBadges();
            return badges;
        }
    }
}
