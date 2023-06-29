using Api.Data.Api.Requests.UserController;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoUser;

namespace Api.Data.GrpcServices.UserService
{
    public class LoginRequestGrpc
    {
        public static async Task<UserLoginResponse> Login(LoginRequest model, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new UserGrpcService.UserGrpcServiceClient(grpcChannel);

                var response = await grpcClient.LoginUserAsync(new UserLoginDTO
                {
                    Username = model.Username,
                    Password = model.Password
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