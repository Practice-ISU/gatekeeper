using Api.Data.Api.Requests.FileController;
using Api.Data.Api.Requests.FolderController;
using Api.Data.Api.Responses.FileController;
using Api.Data.Api.Responses.FolderController;
using Api.Data.GrpcServices.DiscoveryService;
using Api.Data.GrpcServices.FileService;
using Api.Data.GrpcServices.FolderService;
using Api.Data.GrpcServices.UserService;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("file")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserController));

        private async Task<string> GetGrpcChannel(string serviceName)
        {
            string channel = "";

            try
            {
                channel = await GetChannelRequest.GetChannel(serviceName);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in gRPC discovery in {serviceName}: {ex.Message}");
                throw new Exception("Service not available", ex);
            }

            _logger.Info($"{serviceName} channel = {channel}");

            return channel;
        }

        private async Task<long> GetUserIdByToken(string? token)
        {
            var channel = await GetGrpcChannel("UserGrpcService");

            if (channel == "")
            {
                throw new Exception("User service not available");
            }

            _logger.Info($"Try to get user id by token = {token}");

            try
            {
                var responseGrpc = await GetUserByTokenGrpc.GetUserByTokenAsync(token, channel);
                if (!responseGrpc.Details.Success)
                {
                    _logger.Warn($"Failed to get user by token: {responseGrpc.Details.Mess}");
                    throw new Exception("Failed to get user by token");
                }

                _logger.Info($"User id = {responseGrpc.User.Id}");
                return responseGrpc.User.Id;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while getting user id by token: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<AddFileResponse>> AddFileAsync(AddFileRequest request)
        {
            if (!request.IsValid())
            {
                _logger.Warn($"Invalid add file data received for token '{request.Token}'");
                return BadRequest(new AddFileResponse("Invalid add file data"));
            }

            var channel = await GetGrpcChannel("FileGrpcService");

            if (channel == "")
            {
                return BadRequest(new AddFileResponse("Service not available"));
            }

            try
            {
                long userId = await GetUserIdByToken(request.Token);

                var responseGrpc = await AddFileGrpc.AddFile(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    _logger.Error($"Failed to add folder: {responseGrpc.Details.Mess}");
                    return BadRequest(new AddFileResponse(responseGrpc.Details.Mess));
                }

                _logger.Info($"File added successfully");
                return Ok(new AddFileResponse(responseGrpc.File.Id, responseGrpc.File.Filename, responseGrpc.File.FolderId, "File added successfully"));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error during file add: {ex.Message}");
                return BadRequest(new AddFileResponse("Add file failed"));
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult<DeleteFileResponse>> DeleteFileAsync(DeleteFileRequest request)
        {
            if (!request.IsValid())
            {
                _logger.Warn($"Invalid add file data received for token '{request.Token}'");
                return BadRequest(new DeleteFileResponse("Invalid add file data"));
            }

            var channel = await GetGrpcChannel("FileGrpcService");

            if (channel == "")
            {
                return BadRequest(new DeleteFileResponse("Service not available"));
            }

            try
            {
                long userId = await GetUserIdByToken(request.Token);

                var responseGrpc = await DeleteFileGrpc.DeleteFile(request, channel);
                if (!responseGrpc.Success)
                {
                    _logger.Error($"Failed to add folder: {responseGrpc.Mess}");
                    return BadRequest(new DeleteFileResponse(responseGrpc.Mess));
                }

                _logger.Info($"File added successfully");
                return Ok(new DeleteFileResponse("File deleted successfully", true));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error during file add: {ex.Message}");
                return BadRequest(new DeleteFileResponse("Add file failed"));
            }
        }

        [HttpPost]
        [Route("rename")]
        public async Task<ActionResult<RenameFileResponse>> DeleteFileAsync(RenameFileRequest request)
        {
            if (!request.IsValid())
            {
                _logger.Warn($"Invalid add file data received for token '{request.Token}'");
                return BadRequest(new RenameFileResponse("Invalid add file data"));
            }

            var channel = await GetGrpcChannel("FileGrpcService");

            if (channel == "")
            {
                return BadRequest(new RenameFileResponse("Service not available"));
            }

            try
            {
                long userId = await GetUserIdByToken(request.Token);

                var responseGrpc = await RenameFileGrpc.RenameFile(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    _logger.Error($"Failed to add folder: {responseGrpc.Details.Mess}");
                    return BadRequest(new RenameFileResponse(responseGrpc.Details.Mess));
                }

                _logger.Info($"File added successfully");
                return Ok(new RenameFileResponse(responseGrpc.File.Id, responseGrpc.File.Filename, responseGrpc.File.FolderId, "File renamed successfully", true));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error during file add: {ex.Message}");
                return BadRequest(new RenameFileResponse("Add file failed"));
            }
        }
    }
}
