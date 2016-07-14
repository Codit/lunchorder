namespace Lunchorder.Domain.Entities.Configuration
{
    public class LocalAuthentication
    {
        public string AudienceId { get; set; }
        public string AudienceSecret { get; set; }
        public string Issuer { get; set; }
        public bool AllowInsecureHttps { get; set; }
    }
}