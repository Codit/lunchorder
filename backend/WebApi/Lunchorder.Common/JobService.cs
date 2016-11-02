using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.DocumentDb;
using Newtonsoft.Json;
using MenuVendorClosingDateRange = Lunchorder.Domain.Dtos.MenuVendorClosingDateRange;

namespace Lunchorder.Common
{
    public class JobService : IJobService
    {
        private readonly ICacheService _cacheService;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IConfigurationService _configurationService;

        public JobService(ICacheService cacheService, IDatabaseRepository databaseRepository, IConfigurationService configurationService)
        {
            if (cacheService == null) throw new ArgumentNullException(nameof(cacheService));
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _cacheService = cacheService;
            _databaseRepository = databaseRepository;
            _configurationService = configurationService;
        }

        public async Task RemindUsers()
        {
            var utcNow = DateTime.UtcNow;
            if (IsWeekDay(utcNow))
            {
                var menu = await _cacheService.GetMenu();

                if (!IsVenueOpen(utcNow, menu.Vendor.ClosingDateRanges))
                {
                    return;
                }

                var pushTokens = await _databaseRepository.GetPushTokens();
                await SendPushNotification(pushTokens);
            }

        }

        private async Task SendPushNotification(IEnumerable<PushTokenItem> pushTokens)
        {
            var tokens = new List<string>();
            foreach (var pushToken in pushTokens)
            {
                if (pushToken.Token.StartsWith("https://android.googleapis.com/gcm/send"))
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
                    RequestUri = new Uri("https://android.googleapis.com/gcm/send"),
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

        public class RegistrationIdRequest
        {
            [JsonProperty("registration_ids")]
            public IEnumerable<string> RegistrationIds { get; set; }
        }

        /// <summary>
        /// Checks if the current datetime is a weekday
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private bool IsWeekDay(DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            return (dayOfWeek >= DayOfWeek.Monday) && (dayOfWeek <= DayOfWeek.Friday);
        }

        /// <summary>
        /// Checks if venue is not closed at a given datetime
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="vendorClosingDateRanges"></param>
        /// <returns></returns>
        private bool IsVenueOpen(DateTime currentTime, IEnumerable<MenuVendorClosingDateRange> vendorClosingDateRanges)
        {
            foreach (var closedDate in vendorClosingDateRanges)
            {
                if (DateTime.Compare(currentTime, DateTime.Parse(closedDate.From)) > 1 && DateTime.Compare(currentTime, DateTime.Parse(closedDate.Until)) < 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
