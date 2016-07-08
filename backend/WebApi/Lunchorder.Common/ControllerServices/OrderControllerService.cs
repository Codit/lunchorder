using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public OrderControllerService(IMapper mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
            _fixture = new Fixture();
        }

        public async Task<IEnumerable<UserOrderHistory>> GetHistory(string userId)
        {
            var userOrderHistories = _fixture.Create<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>>();
            var userOrderHistoriesDto =_mapper.Map<IEnumerable<Domain.Entities.DocumentDb.UserOrderHistory>, IEnumerable<Domain.Dtos.UserOrderHistory>>(userOrderHistories);
            return await Task.FromResult(userOrderHistoriesDto);
        }

        public Task Delete(Guid orderId)
        {
            return null;
        }

        public Task Add(string userId, IEnumerable<MenuOrder> menuOrders)
        {
            return null;
        }
    }
}