using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that hold an ApiKey
    /// </summary>
    public class AuthenticationLocalElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the audience
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
        /// The value of the audience
        /// </summary>
        [ConfigurationProperty("audienceSecret", IsKey = true, IsRequired = true)]
        public string AudienceSecret
        {
            get
            {
                return base["audienceSecret"] as string;
            }
            set
            {
                base["audienceSecret"] = value;
            }
        }

        /// <summary>
        /// The value of the Audience
        /// </summary>
        [ConfigurationProperty("issuer", IsKey = true, IsRequired = true)]
        public string Issuer
        {
            get
            {
                return base["issuer"] as string;
            }
            set
            {
                base["issuer"] = value;
            }
        }
    }
}