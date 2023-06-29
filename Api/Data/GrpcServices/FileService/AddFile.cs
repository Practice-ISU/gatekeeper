using Api.Data.Api.Requests.FileController;
using Api.Data.Api.Requests.FolderController;
using FileService;
using static FileService.FileService;
using Folderservice;
using Grpc.Core;
using Grpc.Net.Client;

namespace Api.Data.GrpcServices.FileService
{
    public class AddFileGrpc
    {
        public static async Task<FileResponse> AddFile(AddFileRequest model, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new FileServiceClient(grpcChannel);

                var response = await grpcClient.AddFileAsync(new FileAddDTO
                {
                    FileName = model.FileName,
                    FolderId = (long)model.FolderId!,
                    Base64 = model.Base64,
                    Format = model.Format,
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
