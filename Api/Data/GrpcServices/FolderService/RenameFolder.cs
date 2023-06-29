using Api.Data.Api.Requests;
using Folderservice;
using Grpc.Core;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.FolderService
{
    public class RenameFolderGrpc
    {
        public static async Task<FolderResponse> RenameFolder(string? newFolderName, Int64 folderId, Int64 userId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new Folderservice.FolderService.FolderServiceClient(grpcChannel);

                var response = await grpcClient.RenameFolderAsync(new FolderRenameDTO
                {
                    Id = folderId,
                    NewFolderName = newFolderName,
                    UserId = userId
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
