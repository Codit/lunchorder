using System.Collections.Generic;
using Lunchorder.Domain.Entities.Configuration;

namespace Lunchorder.Common.Interfaces
{
    /// <summary>
    /// Interface for the custom configuration file section mezurio
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// List of ApiKeys that will grant access to authorized requests
        /// </summary>
        List<ApiKeyElement> ApiKeys { get; }

        /// <summary>
        /// List of clients that will grant access to the environment
        /// </summary>
        List<ClientElement> Clients { get; }

        /// <summary>
        /// A role that the user must be member of or the authorization will be denied. If left empty, authorization is always granted.
        /// </summary>
        string AuthenticationEndpoint { get; }

        AzureAuthentication AzureAuthentication { get; }

        LocalAuthentication LocalAuthentication { get; }

        DocumentDbInfo DocumentDb { get; }

        ServicebusInfo Servicebus { get; }

        EmailInfo Email { get; }

        CompanyInfo Company { get; }

        List<JobElement> Jobs { get; }

        PushProviderInfo PushProviders { get; }

        AzureStorageInfo AzureStorage { get; }
    }
}