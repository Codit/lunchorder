using System;
using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public MemoryCacheService(IDatabaseRepository databaseRepository)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            _databaseRepository = databaseRepository;
        }

        public async Task<Menu> GetMenu()
        {
            Menu menu;
            var cacheMenu = MemoryCacher.GetValue(Cache.Menu) as Menu;
            if (cacheMenu != null)
            {
                menu = cacheMenu;
            }
            else
            {
                menu = await _databaseRepository.GetEnabledMenu();
            }
            return menu;
        }
    }
}