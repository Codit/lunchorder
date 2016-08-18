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

        /// <summary>
        /// The value of the azure ad graph url
        /// </summary>
        [ConfigurationProperty("baseGraphApiUrl", IsKey = true, IsRequired = true)]
        public string BaseGraphApiUrl
        {
            get
            {
                return base["baseGraphApiUrl"] as string;
            }
            set
            {
                base["baseGraphApiUrl"] = value;
            }
        }

        /// <summary>
        /// The value of the azure ad graph version
        /// </summary>
        [ConfigurationProperty("graphApiVersion", IsKey = true, IsRequired = true)]
        public string GraphApiVersion
        {
            get
            {
                return base["graphApiVersion"] as string;
            }
            set
            {
                base["graphApiVersion"] = value;
            }
        }
    }
}