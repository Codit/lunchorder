using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.Configuration;

namespace Lunchorder.Api.Infrastructure.Services
{
    /// <summary>
    /// Service that exposes properties for the custom configuration file section mezurio
    /// </summary>
    public class ConfigurationService : ConfigurationSection, IConfigurationService
    {
        [ConfigurationProperty("authentication")]
        private AuthenticationElement Authentication
        {
            get { return (AuthenticationElement)this["authentication"]; }
        }

        [ConfigurationProperty("connections")]
        private ConnectionsElement Connections
        {
            get { return (ConnectionsElement)this["connections"]; }
        }

        [ConfigurationProperty("application")]
        private ApplicationElement ApplicationSetting
        {
            get { return (ApplicationElement)this["application"]; }
        }

        [ConfigurationProperty("eventing")]
        private EventingElement Eventing
        {
            get { return (EventingElement)this["eventing"]; }
        }

        [ConfigurationProperty("email")]
        private EmailElement EmailSetting
        {
            get { return (EmailElement)this["email"]; }
        }

        [ConfigurationProperty("jobs")]
        public JobsElementCollection JobsElementCollection
        {
            get { return base["jobs"] as JobsElementCollection; }
        }

        public List<JobElement> Jobs
        {
            get { return JobsElementCollection.ToList(); }
        }

        public EmailInfo Email => new EmailInfo
        {
            ApiKey = EmailSetting.Sendgrid.ApiKey,
            From = EmailSetting.Sendgrid.From,
            Bcc = ParseBcc()
        };

        private IEnumerable<string> ParseBcc()
        {
            IEnumerable<string> bccAddresses = null;
            var bcc = EmailSetting.Sendgrid.Bcc;
            if (!string.IsNullOrEmpty(bcc))
            {
                bccAddresses = bcc.Split(';').AsEnumerable();
            }
            return bccAddresses;
        }


        public CompanyInfo Company => new CompanyInfo
        {
            Name = ApplicationSetting.Company.Name,
            Phone = ApplicationSetting.Company.Phone,
            Website = ApplicationSetting.Company.Website,
            Address = new CompanyAddressInfo
            {
                Street = ApplicationSetting.Company.Address.Street,
                Number = ApplicationSetting.Company.Address.Number,
                PostalCode = ApplicationSetting.Company.Address.PostalCode,
                City = ApplicationSetting.Company.Address.City
            }
        };

        public ServicebusInfo Servicebus => new ServicebusInfo
        {
            ConnectionString = Eventing.Servicebus.ConnectionString,
            Enabled = Eventing.Servicebus.Enabled,
            Topic = Eventing.Servicebus.Topic
        };

        public DocumentDbInfo DocumentDb => new DocumentDbInfo
        {
            Endpoint = Connections.DocumentDb.Endpoint,
            AuthKey = Connections.DocumentDb.AuthKey,
            Database = Connections.DocumentDb.Database,
            Collection = Connections.DocumentDb.Collection
        };

        public DocumentDbInfo DocumentDbAuth => new DocumentDbInfo
        {
            Endpoint = Connections.DocumentDb.Endpoint,
            AuthKey = Connections.DocumentDb.AuthKey,
            Database = Connections.DocumentDb.Database,
            Collection = Connections.DocumentDb.Collection
        };

        public List<ApiKeyElement> ApiKeys
        {
            get { return Authentication.ApiKeysElementCollection.ToList(); }
        }

        public List<ClientElement> Clients
        {
            get { return Authentication.ClientsElementCollection.ToList(); }
        }

        public string AuthenticationEndpoint
        {
            get { return Authentication.AuthenticationEndpoint.Value; }
        }

        public AzureAuthentication AzureAuthentication => new AzureAuthentication
        {
            AudienceId = Authentication.Azure.Audience,
            AllowInsecureHttps = Authentication.AllowInsecureHttp,
            Tenant = Authentication.Azure.Tenant,
            BaseGraphApiUrl = Authentication.Azure.BaseGraphApiUrl,
            GraphApiVersion = Authentication.Azure.GraphApiVersion
        };

        public LocalAuthentication LocalAuthentication => new LocalAuthentication { AudienceId = Authentication.Local.Audience, AllowInsecureHttps = Authentication.AllowInsecureHttp, AudienceSecret = Authentication.Local.AudienceSecret, Issuer = Authentication.Local.Issuer };
    }
}