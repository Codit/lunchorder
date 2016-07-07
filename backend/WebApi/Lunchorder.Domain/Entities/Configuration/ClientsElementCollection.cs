using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Collection of ApiKey Elements
    /// </summary>
    [ConfigurationCollection(typeof(ClientsElementCollection), AddItemName = "client")]
    public class ClientsElementCollection : ConfigurationElementCollection, IEnumerable<ClientElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ClientElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var apiKeyElement = element as ClientElement;
            return apiKeyElement != null ? apiKeyElement.Value : null;
        }

        public ClientElement this[int index]
        {
            get
            {
                return BaseGet(index) as ClientElement;
            }
        }

        #region IEnumerable<ConfigElement> Members

        IEnumerator<ClientElement> IEnumerable<ClientElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, Count)
                select this[i])
                .GetEnumerator();
        }

        #endregion
    }
}