using FileService;
using Grpc.Core;
using Grpc.Net.Client;
using static FileService.FileService;

namespace Api.Data.GrpcServices.FileService
{
    public class GetAllFilesInFolderZipGrpc
    {
        public static async Task<FileAllZipResponse> GetAllFilesInFolderZip(Int64? folderId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new FileServiceClient(grpcChannel);

                var response = await grpcClient.GetAllFilesInFolderZipAsync(new FileGetAllDTO
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
