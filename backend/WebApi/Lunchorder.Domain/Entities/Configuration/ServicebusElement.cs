using System;
using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class CompanyElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"].ToString();
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("phone", IsRequired = true)]
        public string Phone
        {
            get
            {
                return base["phone"].ToString();
            }
            set
            {
                base["phone"] = value;
            }
        }

        [ConfigurationProperty("website", IsRequired = true)]
        public string Website
        {
            get
            {
                return base["website"].ToString();
            }
            set
            {
                base["website"] = value;
            }
        }

        /// <summary>
        /// Company Address
        /// </summary>
        [ConfigurationProperty("address")]
        public CompanyAddressElement Address
        {
            get { return base["address"] as CompanyAddressElement; }
        }
        //<application>
        //  <company name = "" phone="" website="" >
        //    <address street = "" number="" postalCode="" city="" />
        //  </company>
        //</application>
    }

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