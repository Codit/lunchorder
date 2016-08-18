namespace Lunchorder.Domain.Entities.Configuration
{
    public class AzureAuthentication
    {
        public string AudienceId { get; set; }
        public bool AllowInsecureHttps { get; set; }
        public string Tenant { get; set; }
        public string BaseGraphApiUrl { get; set; }
        public string GraphApiVersion { get; set; }
    }
}