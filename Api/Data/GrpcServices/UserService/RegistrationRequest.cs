using Api.Data.Api.Requests;
using Grpc.Net.Client;
using ProtoUser;

namespace Api.Data.GrpcServices.UserService
{
    public class RegistrationRequestGrpc
    {
        public static async Task<UserRegisterResponse> Registration(RegistrationRequest model, string chanel)
        {
            using var channel = GrpcChannel.ForAddress(chanel);
            var client = new UserGrpcService.UserGrpcServiceClient(channel);
            var response = await client.RegisterUserAsync(new UserRegisterDTO { Username = model.Username, Password = model.Password });

            // Console.WriteLine($"Success: {response.Details.Success}, Username: {response.User.Username}");
            channel.ShutdownAsync().Wait();

            return response;
        }
    }
}
