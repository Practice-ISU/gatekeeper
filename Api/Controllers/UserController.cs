﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    using Api.Data.Api.Requests;
    using Api.Data.Api.Responses;
    using System.Threading.Tasks;
    using Data.GrpcServices.UserService;
    using static System.Net.WebRequestMethods;
    using ProtoUser;
    using Api.Data.GrpcServices.DiscoveryService;
    using Discovery;
    using log4net;

    [Route("auth")]
    [ApiController]
    public class UserController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserController));

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync(RegistrationRequest request)
        {
            if (!request.IsValid())
            {
                logger.Warn($"Invalid registration data received for user '{request.Username}'");
                return BadRequest(new RegistrationResponse
                {
                    Token = null,
                    Message = "Invalid registration data",
                    IsSuccess = false
                });
            }

            string channel = "";

            try
            {
                channel = await GetChannelRequest.GetChannel("UserGrpcService");
            }
            catch (Exception ex)
            {
                logger.Error($"Error in gRPC discovery in UserGrpcService: {ex.Message}");
                return BadRequest(new RegistrationResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            logger.Info($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await RegistrationRequestGrpc.Registration(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    logger.Warn($"Failed registration attempt: {responseGrpc.Details.Mess}");
                    return BadRequest(new RegistrationResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                logger.Info("User registered successfully");
                return Ok(new RegistrationResponse
                {
                    Token = responseGrpc.User.Token,
                    Message = "User registered successfully",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                logger.Error($"Error during registration: {ex.Message}");
                return BadRequest(new RegistrationResponse
                {
                    Token = null,
                    Message = "Registration failed.",
                    IsSuccess = false
                });
            }
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (!request.IsValid())
            {
                logger.Warn($"Invalid login data received for user '{request.Username}'");
                return BadRequest(new LoginResponse
                {
                    Token = null,
                    Message = "Invalid login data",
                    IsSuccess = false
                });
            }

            string channel = "";

            try
            {
                channel = await GetChannelRequest.GetChannel("UserGrpcService");
            }
            catch (Exception ex)
            {
                logger.Error($"Error in gRPC discovery: {ex.Message}");
                return BadRequest(new LoginResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            logger.Info($"UserGrpcChannel = {channel}");


            try
            {
                var responseGrpc = await LoginRequestGrpc.Login(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    logger.Error($"Failed login attempt: {responseGrpc.Details.Mess}");
                    return BadRequest(new LoginResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                logger.Info("User login successfully");
                return Ok(new LoginResponse
                {
                    Token = responseGrpc.Token,
                    Message = "Login successful",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                logger.Error($"Error during login: {ex.Message}");
                return BadRequest(new LoginResponse
                {
                    Token = null,
                    Message = "Login failed",
                    IsSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> VerifyTokenAsync(VerifyUserRequest request)
        {
            if (!request.IsValid())
            {
                logger.Error("Invalid verify token request");
                return BadRequest(new VerifyUserResponse
                {
                    Token = null,
                    Message = "Invalid request",
                    IsSuccess = false
                });
            }

            string channel = "";

            try
            {
                channel = await GetChannelRequest.GetChannel("UserGrpcService");
            }
            catch (Exception ex)
            {
                logger.Error($"Error in gRPC discovery: {ex.Message}");
                return BadRequest(new VerifyUserResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            logger.Info($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await VerifyUserGrpc.VerifyUser(request.Token, channel);
                if (!responseGrpc.Details.Success)
                {
                    logger.Error($"Failed token verification attempt: {responseGrpc.Details.Mess}");
                    return BadRequest(new VerifyUserResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                logger.Info($"Token verify successfully");
                return Ok(new VerifyUserResponse
                {
                    Token = responseGrpc.User.Token,
                    Message = "Verification is successful",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                logger.Error($"Error during login: {ex.Message}");
                return BadRequest(new VerifyUserResponse
                {
                    Token = null,
                    Message = "Verification failed",
                    IsSuccess = false
                });
            }
        }
    }
}