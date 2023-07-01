using Api.Data.Api.Requests;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoUser;

namespace Api.Data.GrpcServices.UserService
{
    public class GetUserByTokenGrpc
    {
        public static async Task<UserResponse> GetUserByTokenAsync(string? token, string channel)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty", nameof(token));
            }

            if (string.IsNullOrEmpty(channel))
            {
                throw new ArgumentException("Channel cannot be null or empty", nameof(channel));
            }

            using var grpcChannel = GrpcChannel.ForAddress(channel);
            var grpcClient = new UserGrpcService.UserGrpcServiceClient(grpcChannel);

            try
            {
                var response = await grpcClient.GetUserByTokenAsync(new UserToken
                {
                    Token = token
                });

                return response;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new InvalidOperationException($"Invalid channel URL: {channel}", ex);
            }
        }
    }
}
