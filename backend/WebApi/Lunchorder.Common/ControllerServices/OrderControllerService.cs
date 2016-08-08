using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Eventing;
using Newtonsoft.Json;
using Menu = Lunchorder.Domain.Dtos.Menu;
using UserOrderHistory = Lunchorder.Domain.Dtos.UserOrderHistory;
using UserOrderHistoryEntry = Lunchorder.Domain.Dtos.UserOrderHistoryEntry;

namespace Lunchorder.Common.ControllerServices
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;
        private readonly IEventingService _eventingService;

        public OrderControllerService(IDatabaseRepository databaseRepository, IMapper mapper, IEventingService eventingService)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (eventingService == null) throw new ArgumentNullException(nameof(eventingService));
            _databaseRepository = databaseRepository;
            _mapper = mapper;
            _eventingService = eventingService;
        }

        public async Task<IEnumerable<UserOrderHistory>> GetHistory(string userId)
        {
            //var userOrderHistories = _fixture.Create<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>>();
            //var userOrderHistoriesDto =_mapper.Map<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>, IEnumerable<Domain.Dtos.UserOrderHistory>>(userOrderHistories);
            //return await Task.FromResult(userOrderHistoriesDto);
            throw new NotImplementedException();
        }

        public Task Delete(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task Add(string userId, string userName, IEnumerable<MenuOrder> menuOrders)
        {
            var vendorId = await GetVendorId();
            var menuOrderHistoryEntries = _mapper.Map<IEnumerable<MenuOrder>, IEnumerable<UserOrderHistoryEntry>>(menuOrders);

            var userOrderHistory = new UserOrderHistory { Id = Guid.NewGuid(), OrderTime = DateTime.UtcNow, Entries = menuOrderHistoryEntries };

            await _databaseRepository.AddOrder(userId, userName, vendorId, new DateGenerator().GenerateDateFormat(DateTime.UtcNow), userOrderHistory);
            _eventingService.SendMessage(new Message(ServicebusType.AddUserOrder, JsonConvert.SerializeObject(userOrderHistory)));
        }

        public async Task EmailVendorHistory(DateTime dateTime)
        {
            var vendorHistory = await GetVendorHistory(dateTime);
            var html = HtmlHelper.CreateVendorHistory(vendorHistory);
        }

        public async Task<VendorOrderHistory> GetVendorHistory(DateTime dateTime)
        {
            var vendorId = await GetVendorId();
            var vendorOrderHistory = await _databaseRepository.GetVendorOrder(new DateGenerator().GenerateDateFormat(dateTime), vendorId);
            return vendorOrderHistory;
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