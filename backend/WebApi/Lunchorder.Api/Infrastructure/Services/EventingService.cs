using System.Collections.Generic;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.Eventing;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class EventingService : IEventingService
    {
        private readonly IEnumerable<IMessagingService> _messagingServices;

        public EventingService(IEnumerable<IMessagingService> messagingServices)
        {
            _messagingServices = messagingServices;
        }

        public void SendMessage(Message message)
        {
            if (_messagingServices != null)
            {
                foreach (var messagingService in _messagingServices)
                {
                    messagingService.SendMessageAsync(message);
                }
            }
        }
    }
}