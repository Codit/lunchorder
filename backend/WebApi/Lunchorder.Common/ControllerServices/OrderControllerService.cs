using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.Eventing;
using Lunchorder.Domain.Exceptions;
using Newtonsoft.Json;
using UserOrderHistory = Lunchorder.Domain.Dtos.UserOrderHistory;
using UserOrderHistoryEntry = Lunchorder.Domain.Dtos.UserOrderHistoryEntry;

namespace Lunchorder.Common.ControllerServices
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;
        private readonly IEventingService _eventingService;
        private readonly IEmailService _emailService;
        private readonly ICacheService _cacheService;

        public OrderControllerService(IConfigurationService configurationService, IDatabaseRepository databaseRepository, IMapper mapper, IEventingService eventingService, IEmailService emailService, ICacheService cacheService)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (eventingService == null) throw new ArgumentNullException(nameof(eventingService));
            if (emailService == null) throw new ArgumentNullException(nameof(emailService));
            if (cacheService == null) throw new ArgumentNullException(nameof(cacheService));
            _configurationService = configurationService;
            _databaseRepository = databaseRepository;
            _mapper = mapper;
            _eventingService = eventingService;
            _emailService = emailService;
            _cacheService = cacheService;
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

        public async Task Add(string userId, string userName, string fullName, IEnumerable<MenuOrder> menuOrders)
        {
            var menu = await _cacheService.GetMenu();

            if (!string.IsNullOrEmpty(menu.Vendor.SubmitOrderTime))
            {
                DateTime parsedOrderDateTime;
                if (DateTime.TryParse(menu.Vendor.SubmitOrderTime, out parsedOrderDateTime))
                {
                    if (TimeSpan.Compare(parsedOrderDateTime.TimeOfDay, DateTime.UtcNow.TimeOfDay) <= 0)
                    {
                        throw new BusinessException($"Sorry, you were too late to submit your order for today");
                    }
                }
            }

            var menuOrderHistoryEntries =
                _mapper.Map<IEnumerable<MenuOrder>, IEnumerable<UserOrderHistoryEntry>>(menuOrders);

            var userOrderHistory = new UserOrderHistory
            {
                Id = Guid.NewGuid(),
                OrderTime = DateTime.UtcNow,
                Entries = menuOrderHistoryEntries
            };

            await
                _databaseRepository.AddOrder(userId, userName, menu.Vendor.Id,
                    new DateGenerator().GenerateDateFormat(DateTime.UtcNow), userOrderHistory, fullName);
            _eventingService.SendMessage(new Message(ServicebusType.AddUserOrder,
                JsonConvert.SerializeObject(userOrderHistory)));
        }

        public async Task<string> GetEmailVendorHistory(DateTime dateTime)
        {
            var vendorHistory = await GetVendorHistory(dateTime);
            var html = HtmlHelper.CreateVendorHistory(_configurationService, vendorHistory);
            return html;
        }

        public async Task SendEmailVendorHistory(DateTime dateTime)
        {
            // todo, check if already sent for this date

            var menu = await _cacheService.GetMenu();
            var vendorHistory = await GetVendorHistory(dateTime);
            var htmlOutput = HtmlHelper.CreateVendorHistory(_configurationService, vendorHistory);
            await _emailService.SendHtmlEmail($"Order for {dateTime.ToString("D")}", menu.Vendor.Address.Email, htmlOutput);

            // todo, set vendor history sent to true
        }


        public async Task<VendorOrderHistory> GetVendorHistory(DateTime dateTime)
        {
            var vendorId = await GetVendorId();
            var vendorOrderHistory = await _databaseRepository.GetVendorOrder(new DateGenerator().GenerateDateFormat(dateTime), vendorId);
            return vendorOrderHistory;
        }

        private async Task<string> GetVendorId()
        {
            var menu = await _cacheService.GetMenu();
            return menu.Vendor.Id;
        }
    }
}