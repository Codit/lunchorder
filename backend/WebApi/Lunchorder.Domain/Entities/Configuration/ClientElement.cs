using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that hold a client id
    /// </summary>
    public class ClientElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the client
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