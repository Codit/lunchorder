namespace Lunchorder.Domain.Entities.Configuration
{
    public class DocumentDbInfo
    {
        public string Endpoint { get; set; }
        public string AuthKey { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}