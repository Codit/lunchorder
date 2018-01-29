using System;
using System.Threading.Tasks;
using Lunchorder.Common;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class MenuService : IMenuService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public MenuService(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(databaseRepository));
        }

        public async Task<Menu> GetActiveMenu()
        {
            if (MemoryCacher.GetValue(Cache.Menu) is Menu cacheMenu) return cacheMenu;

            var menu = await _databaseRepository.GetEnabledMenu();
            if (menu != null)
                MemoryCacher.Add(Cache.Menu, menu);

            return menu;
        }
    }
}