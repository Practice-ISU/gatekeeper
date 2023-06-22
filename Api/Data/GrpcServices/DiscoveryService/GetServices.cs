using Discovery;
using Grpc.Net.Client;
using ProtoUser;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;

namespace Api.Data.GrpcServices.DiscoveryService
{
    public class GetServicesRequest
    {
        private static readonly string _channel = ConfigurationHelper.GetDiscoveryChannel();
        public static async Task<GetServicesResponse> GetServices()
        {
            using var channel = GrpcChannel.ForAddress(_channel);
            var client = new Discovery.DiscoveryService.DiscoveryServiceClient(channel);
            var response = await client.GetServicesAsync(new Discovery.GetServicesRequest { });
            
            return response;
        }
    }
}
