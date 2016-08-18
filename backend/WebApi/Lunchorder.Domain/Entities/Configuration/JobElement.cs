using System;
using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    /// <summary>
    /// Element that hold a job
    /// </summary>
    public class JobElement : ConfigurationElement
    {
        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
            set
            {
                base["name"] = value;
            }
        }

        /// <summary>
        /// The value of the ApiKey
        /// </summary>
        [ConfigurationProperty("enabled", IsKey = false, IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return Convert.ToBoolean(base["enabled"]);
            }
            set
            {
                base["enabled"] = value;
            }
        }
    }
}