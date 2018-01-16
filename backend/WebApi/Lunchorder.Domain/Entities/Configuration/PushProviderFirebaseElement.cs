using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that holds a firebase push provider
    /// </summary>
    public class PushProviderFirebaseElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("apiKey", IsKey = true, IsRequired = false)]
        public string ApiKey
        {
            get
            {
                return base["apiKey"] as string;
            }
            set
            {
                base["apiKey"] = value;
            }
        }
    }
}