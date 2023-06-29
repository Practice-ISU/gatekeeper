using Api.Data.Api.Requests.UserController;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoUser;

namespace Api.Data.GrpcServices.UserService
{
    public class RegistrationRequestGrpc
    {
        public static async Task<UserRegisterResponse> Registration(RegistrationRequest model, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new UserGrpcService.UserGrpcServiceClient(grpcChannel);

                var response = await grpcClient.RegisterUserAsync(new UserRegisterDTO
                {
                    Username = model.Username,
                    Password = model.Password
                });

                return response;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new ArgumentException($"Invalid channel URL: {channel}", nameof(channel));
            }
            finally
            {
                await grpcChannel?.ShutdownAsync()!;
            }
        }
    }
}
