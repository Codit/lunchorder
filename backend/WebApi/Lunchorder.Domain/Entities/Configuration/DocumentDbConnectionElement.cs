using System.Configuration;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class DocumentDbConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("endpoint", IsRequired = true)]
        public string Endpoint
        {
            get
            {
                return base["endpoint"] as string;
            }
            set
            {
                base["endpoint"] = value;
            }
        }

        [ConfigurationProperty("authKey", IsRequired = true)]
        public string AuthKey
        {
            get
            {
                return base["authKey"] as string;
            }
            set
            {
                base["authKey"] = value;
            }
        }

        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get
            {
                return base["database"] as string;
            }
            set
            {
                base["database"] = value;
            }
        }

        [ConfigurationProperty("collection", IsRequired = true)]
        public string Collection
        {
            get
            {
                return base["collection"] as string;
            }
            set
            {
                base["collection"] = value;
            }
        }
    }
}