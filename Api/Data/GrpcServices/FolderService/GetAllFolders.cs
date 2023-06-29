using Folderservice;
using Grpc.Core;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.FolderService
{
    public class GetAllFoldersGrpc
    {
        public static async Task<AllFoldersResponse> GetAllFolders(Int64 userId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new Folderservice.FolderService.FolderServiceClient(grpcChannel);

                var response = await grpcClient.GetAllFoldersAsync(new UserId
                {
                    UserId_ = userId,
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
