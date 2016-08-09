namespace Lunchorder.Domain.Entities.Configuration
{
    public class ServicebusInfo
    {
        public string ConnectionString { get; set; }
        public bool Enabled { get; set; }
        public string Topic { get; set; }
    }
}