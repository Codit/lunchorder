using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Collection of ApiKey Elements
    /// </summary>
    [ConfigurationCollection(typeof(ApiKeysElementCollection), AddItemName = "apiKey")]
    public class ApiKeysElementCollection : ConfigurationElementCollection, IEnumerable<ApiKeyElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiKeyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var apiKeyElement = element as ApiKeyElement;
            return apiKeyElement != null ? apiKeyElement.Value : null;
        }

        public ApiKeyElement this[int index]
        {
            get
            {
                return BaseGet(index) as ApiKeyElement;
            }
        }

        #region IEnumerable<ConfigElement> Members

        IEnumerator<ApiKeyElement> IEnumerable<ApiKeyElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, Count)
                    select this[i])
                    .GetEnumerator();
        }

        #endregion
    }
}
