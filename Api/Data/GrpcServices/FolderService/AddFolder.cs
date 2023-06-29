using Api.Data.Api.Requests.FolderController;
using Discovery;
using Folderservice;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoUser;
using System.Threading.Channels;

namespace Api.Data.GrpcServices.FolderService
{
    public class AddFolderRequestGrpc
    {
        public static async Task<FolderResponse> AddFolder(AddFolderRequest model, Int64 userId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new Folderservice.FolderService.FolderServiceClient(grpcChannel);

                var response = await grpcClient.AddFolderAsync(new FolderAddDTO
                {
                    UserId = userId,
                    FolderName = model.FolderName
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
