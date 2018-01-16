using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos.Requests;
using Newtonsoft.Json;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class PushTokenService : IPushTokenService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IDatabaseRepository _databaseRepository;

        public PushTokenService(IConfigurationService configurationService, IDatabaseRepository databaseRepository)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(databaseRepository));
        }

        public async Task SendPushNotification()
        {
            var pushTokens = await _databaseRepository.GetPushTokens();

            var tokens = new List<string>();
            foreach (var pushToken in pushTokens)
            {
                if (pushToken.Token.StartsWith(PushTokenConstants.PushGmcUrl))
                {
                    var splitTokenUrl = pushToken.Token.Split('/');
                    tokens.Add(splitTokenUrl[splitTokenUrl.Length - 1]);
                }
            }

            using (var client = new HttpClient())
            {
                var registrationIdsRequest = new RegistrationIdRequest { RegistrationIds = tokens };
                var jsonRequest = JsonConvert.SerializeObject(registrationIdsRequest);
                var apiKeyHeader = $"key={_configurationService.PushProviders.Firebase.ApiKey}";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(PushTokenConstants.PushGmcUrl),
                    Method = HttpMethod.Post,
                    Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"),
                };

                request.Headers.TryAddWithoutValidation("Authorization", apiKeyHeader);

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                }
            }
        }
    }
}