namespace Api
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationHelper
    {
        private static readonly IConfiguration Configuration;

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string GetDiscoveryChannel()
        {
            string? discoveryChannel = Configuration.GetValue<string>("DiscoveryChannel");
            if (discoveryChannel == null)
            {
                throw new InvalidOperationException("\"DiscoveryChannel\" not in config (appsettings.json)");
            }

            return discoveryChannel;
        }

    }
}
