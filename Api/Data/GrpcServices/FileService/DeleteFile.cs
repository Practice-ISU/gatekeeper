using Api.Data.Api.Requests.FileController;
using FileService;
using Grpc.Core;
using Grpc.Net.Client;
using static FileService.FileService;

namespace Api.Data.GrpcServices.FileService
{
    public class DeleteFileGrpc
    {
        public static async Task<Details> DeleteFile(DeleteFileRequest model, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new FileServiceClient(grpcChannel);

                var response = await grpcClient.DeleteFileAsync(new FileDeleteDTO
                {
                    Id = (long)model.FileId!
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
