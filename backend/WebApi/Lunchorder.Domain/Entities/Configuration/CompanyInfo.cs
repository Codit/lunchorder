namespace Lunchorder.Domain.Entities.Configuration
{
    public class CompanyInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public CompanyAddressInfo Address { get; set; }
    }
}