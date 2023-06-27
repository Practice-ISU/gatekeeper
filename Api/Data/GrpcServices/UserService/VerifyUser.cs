using Api.Data.Api.Requests;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoUser;

namespace Api.Data.GrpcServices.UserService
{
    public class VerifyUserGrpc
    {
        public static async Task<UserVerifyResponse> VerifyUser(string? token, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new UserGrpcService.UserGrpcServiceClient(grpcChannel);

                var response = await grpcClient.VerifyUserAsync(new UserToken
                {
                    Token = token
                });

                return response;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new ArgumentException($"Invalid channel url: {channel}", nameof(channel));
            }
            finally
            {
                await grpcChannel?.ShutdownAsync()!;
            }
        }
    }
}
