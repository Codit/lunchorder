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
        };

        public LocalAuthentication LocalAuthentication => new LocalAuthentication { AudienceId = Authentication.Local.Audience, AllowInsecureHttps = Authentication.AllowInsecureHttp, AudienceSecret = Authentication.Local.AudienceSecret, Issuer = Authentication.Local.Issuer };
    }
}