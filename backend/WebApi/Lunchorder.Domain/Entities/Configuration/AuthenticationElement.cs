using System;
using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
        /// Authentication element in the custom mezurio configuration element
        /// </summary>
        public class AuthenticationElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("allowInsecureHttp", IsKey = true, IsRequired = true)]
        public bool AllowInsecureHttp
        {
            get
            {
                return Convert.ToBoolean(base["allowInsecureHttp"]);
            }
            set
            {
                base["allowInsecureHttp"] = value;
            }
        }

        /// <summary>
        /// The collection of Clients
        /// </summary>
        [ConfigurationProperty("clients")]
        public ClientsElementCollection ClientsElementCollection
        {
            get { return base["clients"] as ClientsElementCollection; }
        }

        /// <summary>
        /// The collection of ApiKeys
        /// </summary>
        [ConfigurationProperty("apiKeys")]
        public ApiKeysElementCollection ApiKeysElementCollection
        {
            get { return base["apiKeys"] as ApiKeysElementCollection; }
        }

        /// <summary>
        /// The collection of ApiKeys
        /// </summary>
        [ConfigurationProperty("endpoint")]
        public AuthenticationEndpointElement AuthenticationEndpoint
        {
            get { return base["endpoint"] as AuthenticationEndpointElement; }
        }

        /// <summary>
        /// Azure AD details
        /// </summary>
        [ConfigurationProperty("local")]
        public AuthenticationLocalElement Local
        {
            get { return base["local"] as AuthenticationLocalElement; }
        }

        /// <summary>
        /// Azure AD details
        /// </summary>
        [ConfigurationProperty("azure")]
        public AuthenticationAzureElement Azure
        {
            get { return base["azure"] as AuthenticationAzureElement; }
        }
    }
}