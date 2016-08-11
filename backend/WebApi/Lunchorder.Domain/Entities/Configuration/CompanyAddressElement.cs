using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class CompanyAddressElement : ConfigurationElement
    {
        [ConfigurationProperty("street", IsRequired = true)]
        public string Street
        {
            get
            {
                return base["street"].ToString();
            }
            set
            {
                base["street"] = value;
            }
        }

        [ConfigurationProperty("number", IsRequired = true)]
        public string Number
        {
            get
            {
                return base["number"].ToString();
            }
            set
            {
                base["number"] = value;
            }
        }

        [ConfigurationProperty("postalCode", IsRequired = true)]
        public string PostalCode
        {
            get
            {
                return base["postalCode"].ToString();
            }
            set
            {
                base["postalCode"] = value;
            }
        }

        [ConfigurationProperty("city", IsRequired = true)]
        public string City
        {
            get
            {
                return base["city"].ToString();
            }
            set
            {
                base["city"] = value;
            }
        }
    }
}