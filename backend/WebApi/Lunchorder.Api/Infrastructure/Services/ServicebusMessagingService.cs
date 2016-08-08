using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class ServicebusMessagingService : IMessagingService
    {
        private readonly IConfigurationService _configurationService;

        private string TopicName => _configurationService.Servicebus.Topic;
        private string ConnectionString => _configurationService.Servicebus.ConnectionString;

        public ServicebusMessagingService(IConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _configurationService = configurationService;
            CreateTopic();
        }

        /// <summary>
        /// Create the topic if it does not exist already.
        /// </summary>
        public void CreateTopic()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);

            if (!namespaceManager.TopicExists(TopicName))
            {
                namespaceManager.CreateTopic(TopicName);
            }
        }

        public async Task SendMessageAsync(Domain.Entities.Eventing.Message message)
        {
            var client = TopicClient.CreateFromConnectionString(ConnectionString, TopicName);
            await client.SendAsync(new BrokeredMessage());
        }
    }
}