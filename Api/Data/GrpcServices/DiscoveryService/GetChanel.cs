using Discovery;
using Grpc.Core;
using static Discovery.DiscoveryService;
using System.Threading.Channels;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.DiscoveryService
{
    public class GetChannelRequest
    {
        private static readonly string _channel = ConfigurationHelper.GetDiscoveryChannel();

        public static async Task<string> GetChannel(string serviceName)
        {
            Console.WriteLine($"Start GetChannel with Discovery channel = {_channel}");

            ChannelBase? channel = null;
            DiscoveryServiceClient? client = null;

            try
            {
                channel = GrpcChannel.ForAddress(_channel);

                client = new DiscoveryServiceClient(channel);

                var response = await client.GetChannelAsync(new ServiceNameRequest { ServiceName = serviceName });

                Console.WriteLine($"Get values = {response.ServiceName}, {response.Channel}, {response.Status}");

                if (response.Status == ServiceInfo.Types.Status.Down)
                {
                    throw new Exception("Service not available");
                }

                Console.WriteLine($"{response.ServiceName} channel from discovery = {response.Channel}");
                return "http://" + response.Channel;
            }
            catch (RpcException ex)
            {
                throw new Exception("Service not available", ex);
            }
            finally
            {
                await channel?.ShutdownAsync()!;
            }
        }
    }
}