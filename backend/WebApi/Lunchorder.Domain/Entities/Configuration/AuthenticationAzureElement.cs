using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that holds a tenant
    /// </summary>
    public class AuthenticationAzureElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("tenant", IsKey = true, IsRequired = true)]
        public string Tenant
        {
            get
            {
                return base["tenant"] as string;
            }
            set
            {
                base["tenant"] = value;
            }
        }

        /// <summary>
        /// The value of the Audience
        /// </summary>
        [ConfigurationProperty("audience", IsKey = true, IsRequired = true)]
        public string Audience
        {
            get
            {
                return base["audience"] as string;
            }
            set
            {
                base["audience"] = value;
            }
        }
    }
}