namespace Lunchorder.Domain.Entities.Configuration
{
    public class CompanyAddressInfo
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"{Street} {Number}, {PostalCode} {City}";
        }
    }
}