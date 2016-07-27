using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.DocumentDb;
using Ploeh.AutoFixture;
using Menu = Lunchorder.Domain.Dtos.Menu;
using UserOrderHistory = Lunchorder.Domain.Dtos.UserOrderHistory;
using UserOrderHistoryEntry = Lunchorder.Domain.Dtos.UserOrderHistoryEntry;

namespace Lunchorder.Common.ControllerServices
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public OrderControllerService(IDatabaseRepository databaseRepository, IMapper mapper)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _databaseRepository = databaseRepository;
            _mapper = mapper;
            _fixture = new Fixture();
        }

        public async Task<IEnumerable<UserOrderHistory>> GetHistory(string userId)
        {
            //var userOrderHistories = _fixture.Create<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>>();
            //var userOrderHistoriesDto =_mapper.Map<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>, IEnumerable<Domain.Dtos.UserOrderHistory>>(userOrderHistories);
            //return await Task.FromResult(userOrderHistoriesDto);
            return null;
        }

        public Task Delete(Guid orderId)
        {
            return null;
        }

        public async Task Add(string userId, string userName, IEnumerable<MenuOrder> menuOrders)
        {
            var vendorId = await GetVendorId();
            var menuOrderHistoryEntries = _mapper.Map<IEnumerable<MenuOrder>, IEnumerable<UserOrderHistoryEntry>>(menuOrders);

            // todo add userid to object in dbrepo after map to docdb entity
            var userOrderHistory = new UserOrderHistory { Id = Guid.NewGuid(), OrderTime = DateTime.UtcNow, Entries = menuOrderHistoryEntries };
            //var vendorOrderHistory = new VendorOrderHistory {  }
            await _databaseRepository.AddOrder(userId, userName, vendorId, new DateGenerator().GenerateDateFormat(DateTime.UtcNow),  userOrderHistory);

        }

        private async Task<string> GetVendorId()
        {
            string vendorId;
            var cacheMenu = MemoryCacher.GetValue(Cache.Menu) as Menu;
            if (cacheMenu != null)
            {
                vendorId = cacheMenu.Vendor.Id;
            }
            else
            {
                var menu = await _databaseRepository.GetEnabledMenu();
                vendorId = menu.Vendor.Id;
            }
            return vendorId;
        }
    }
}