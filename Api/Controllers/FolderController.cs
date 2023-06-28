﻿using Api.Data.Api.Requests;
using Api.Data.Api.Responses;
using Api.Data.GrpcServices.DiscoveryService;
using Api.Data.GrpcServices.FolderService;
using Api.Data.GrpcServices.UserService;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("folder")]
    public class FolderController : Controller
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
        public async Task<ActionResult<AddFolderResponse>> AddFolderAsync(AddFolderRequest request)
        {
            if (!request.IsValid())
            {
                _logger.Warn($"Invalid add folder data received for token '{request.Token}'");
                return BadRequest(new AddFolderResponse("Invalid add folder data"));
            }

            var channel = await GetGrpcChannel("FolderGrpcService");

            if (channel == "")
            {
                return BadRequest(new RegistrationResponse("Service not available"));
            }

            try
            {
                long userId = await GetUserIdByToken(request.Token);

                var responseGrpc = await AddFolderRequestGrpc.AddFolder(request, userId, channel);
                if (!responseGrpc.Details.Success)
                {
                    _logger.Error($"Failed to add folder: {responseGrpc.Details.Mess}");
                    return BadRequest(new AddFolderResponse(responseGrpc.Details.Mess));
                }

                _logger.Info($"Folder added successfully");
                return Ok(new AddFolderResponse(responseGrpc.Folder.Id, "Folder added successfully"));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error during folder add: {ex.Message}");
                return BadRequest(new VerifyUserResponse("Add folder failed"));
            }
        }
    }
}
