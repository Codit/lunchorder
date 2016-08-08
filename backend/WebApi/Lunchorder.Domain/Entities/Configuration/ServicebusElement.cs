using System;
using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class ServicebusElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", IsRequired = true)]
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

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return base["connectionString"].ToString();
            }
            set
            {
                base["connectionString"] = value;
            }
        }

        [ConfigurationProperty("topic", IsRequired = true)]
        public string Topic
        {
            get
            {
                return base["topic"].ToString();
            }
            set
            {
                base["topic"] = value;
            }
        }
    }
}