using Discovery;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.DiscoveryService
{
    public class GetChannelRequest
    {
        private static readonly string _channel = ConfigurationHelper.GetDiscoveryChannel();

        public static async Task<string>? GetChannel(string serviceName)
        {
            using var channel = GrpcChannel.ForAddress(_channel);
            var client = new Discovery.DiscoveryService.DiscoveryServiceClient(channel);
            var response = await client.GetChannelAsync(new ServiceNameRequest { ServiceName = serviceName });

            if (response.Status == ServiceInfo.Types.Status.Down)
            {
                throw new Exception("Service not available");
            }

            return response.Channel;
        }
    }
}