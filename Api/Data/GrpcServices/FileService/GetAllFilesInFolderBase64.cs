using FileService;
using Folderservice;
using Grpc.Core;
using Grpc.Net.Client;
using static FileService.FileService;

namespace Api.Data.GrpcServices.FileService
{
    public class GetAllFilesInFolderBase64Grpc
    {
        public static async Task<FileAllBase64Response> GetAllFilesInFolderBase64(Int64? folderId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new FileServiceClient(grpcChannel);

                var response = await grpcClient.GetAllFilesInFolderBase64Async(new FileGetAllDTO
                {
                    FolderId = (long)folderId!,
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
