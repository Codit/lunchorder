using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
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
            var badges = MemoryCacher.GetValue(Cache.Badges) as IEnumerable<Badge>;
            if (badges != null)
            return badges;

            badges = await _databaseRepository.GetBadges();
            MemoryCacher.Add(Cache.Badges, badges);
            return badges;
        }
    }
}
