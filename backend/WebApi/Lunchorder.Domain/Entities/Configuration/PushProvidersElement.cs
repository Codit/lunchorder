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

    /// <summary>
    /// Element that holds push providers
    /// </summary>
    public class PushProvidersElement : ConfigurationElement
    {
        /// <summary>
        /// firebase provider
        /// </summary>
        [ConfigurationProperty("firebase", IsKey = true, IsRequired = false)]
        public PushProviderFirebaseElement Firebase
        {
            get
            {
                return base["firebase"] as PushProviderFirebaseElement;
            }
            set
            {
                base["firebase"] = value;
            }
        }
    }
}