using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Eventing element in the custom lunchorder configuration element
    /// </summary>
    public class EventingElement : ConfigurationElement
    {
        /// <summary>
        /// Microsoft Azure Servicebus
        /// </summary>
        [ConfigurationProperty("servicebus")]
        public ServicebusElement Servicebus
        {
            get { return base["servicebus"] as ServicebusElement; }
        }
    }
}