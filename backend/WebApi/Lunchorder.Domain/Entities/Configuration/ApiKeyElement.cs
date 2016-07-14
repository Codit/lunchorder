using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that hold an ApiKey
    /// </summary>
    public class ApiKeyElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
        public string Value
        {
            get
            {
                return base["value"] as string;
            }
            set
            {
                base["value"] = value;
            }
        }
    }
}