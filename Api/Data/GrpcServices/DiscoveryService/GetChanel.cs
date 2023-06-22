using Discovery;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.DiscoveryService
{
    public class GetChannelRequest
    {
        private static readonly string _channel = ConfigurationHelper.GetDiscoveryChannel();

        public static async Task<string>? GetChannel(string serviceName)
        {
            Console.WriteLine($"Start GetChannel with Discovery chanel = {_channel}");

            using GrpcChannel channel = GrpcChannel.ForAddress(_channel);
            Console.WriteLine("GrpcChannel add");

            var client = new Discovery.DiscoveryService.DiscoveryServiceClient(channel);
            Console.WriteLine("Client created");

            var response = await client.GetChannelAsync(new ServiceNameRequest { ServiceName = serviceName });
            Console.WriteLine($"Get values = {response.ServiceName}, {response.Channel}, {response.Status}");

            if (response.Status == ServiceInfo.Types.Status.Down)
            {
                throw new Exception("Service not available");
            }

            channel.ShutdownAsync().Wait();
            Console.WriteLine($"{response.ServiceName} chanel from discovery = {response.Channel}");
            return "http://" + response.Channel;
        }
    }
}