using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class MenuControllerService : IMenuControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ILogger _logger;
        private readonly IMenuService _menuService;
        private readonly Fixture _fixture;

        public MenuControllerService(IDatabaseRepository databaseRepository, ILogger logger, IMenuService menuService)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(databaseRepository));
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _logger = logger;
            _fixture = new Fixture();
        }

        public async Task<bool> SendEmail()
        {
            return await Task.FromResult(_fixture.Create<bool>());
        }

        public async Task<Menu> GetActiveMenu()
        {
            return await _menuService.GetActiveMenu();
        }

        public async Task Add(Menu menu)
        {
            await _databaseRepository.AddMenu(menu);
            ClearMenuCache();
        }

        public async Task Update(Menu menu)
        {
            await _databaseRepository.UpdateMenu(menu);
            ClearMenuCache();
        }

        public async Task Delete(string menuId)
        {
            await _databaseRepository.DeleteMenu(menuId);
            ClearMenuCache();
        }

        public async Task SetActive(string menuId)
        {
            await _databaseRepository.SetActiveMenu(menuId);
        }

        private void ClearMenuCache()
        {
            MemoryCacher.Delete(Cache.Menu);
        }
    }
}