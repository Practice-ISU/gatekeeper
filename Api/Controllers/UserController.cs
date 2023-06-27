using Microsoft.AspNetCore.Http;
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

        private async Task<string> GetGRPCChannel(string serviceName)
        {
            string channel = "";

            try
            {
                channel = await GetChannelRequest.GetChannel(serviceName);
            }
            catch (Exception ex)
            {
                logger.Error($"Error in gRPC discovery in {serviceName}: {ex.Message}");
                throw new Exception("Service not available");
            }

            logger.Info($"{serviceName} channel = {channel}");

            return channel;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync(RegistrationRequest request)
        {
            if (!request.IsValid())
            {
                logger.Warn($"Invalid registration data received for user '{request.Username}'");
                return BadRequest(new RegistrationResponse("Invalid registration data"));
            }


            var channel = await GetGRPCChannel("UserGrpcService");

            if (channel == "")
            {
                return BadRequest(new RegistrationResponse("Service not available"));
            }

            try
            {
                var responseGrpc = await RegistrationRequestGrpc.Registration(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    logger.Warn($"Failed registration attempt: {responseGrpc.Details.Mess}");
                    return BadRequest(new RegistrationResponse(responseGrpc.Details.Mess));
                }

                logger.Info("User registered successfully");
                return Ok(new RegistrationResponse(responseGrpc.User.Token, "User registered successfully"));
            }
            catch (Exception ex)
            {
                logger.Error($"Error during registration: {ex.Message}");
                return BadRequest("Registration failed");
            }
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (!request.IsValid())
            {
                logger.Warn($"Invalid login data received for user '{request.Username}'");
                return BadRequest(new LoginResponse("Invalid login data"));
            }

            var channel = await GetGRPCChannel("UserGrpcService");

            if (channel == "")
            {
                return BadRequest(new LoginResponse("Service not available"));
            }

            logger.Info($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await LoginRequestGrpc.Login(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    logger.Error($"Failed login attempt: {responseGrpc.Details.Mess}");
                    return BadRequest(new LoginResponse(responseGrpc.Details.Mess));
                }

                logger.Info("User login successfully");
                return Ok(new LoginResponse(responseGrpc.Token, "Login successful"));
            }
            catch (Exception ex)
            {
                logger.Error($"Error during login: {ex.Message}");
                return BadRequest(new LoginResponse("Login failed"));
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