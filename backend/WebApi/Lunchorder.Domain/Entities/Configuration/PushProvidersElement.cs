using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
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