using FileService;
using Grpc.Core;
using Grpc.Net.Client;
using static FileService.FileService;

namespace Api.Data.GrpcServices.FileService
{
    public class GetFileGrpc
    {
        public static async Task<FileResponse> GetFile(Int64? fileId, string channel)
        {
            ChannelBase? grpcChannel = null;
            try
            {
                grpcChannel = GrpcChannel.ForAddress(channel);
                var grpcClient = new FileServiceClient(grpcChannel);

                var response = await grpcClient.GetFileAsync(new FileGetDTO
                {
                    Id = (long)fileId!,
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
