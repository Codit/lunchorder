namespace Lunchorder.Api.Configuration.Mapper
{
    public class MapInitialization
    {
        public static void InitializeMapper()
        {
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}