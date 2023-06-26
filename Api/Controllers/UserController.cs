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

    [Route("auth")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync(RegistrationRequest request)
        {
            if (!request.IsValid())
            {
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
                Console.WriteLine($"Error in gRPC discovery: {ex.Message}");
                return BadRequest(new RegistrationResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            Console.WriteLine($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await RegistrationRequestGrpc.Registration(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    return BadRequest(new RegistrationResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                return Ok(new RegistrationResponse
                {
                    Token = responseGrpc.User.Token,
                    Message = "User registered successfully",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
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
                Console.WriteLine($"Error in gRPC discovery: {ex.Message}");
                return BadRequest(new LoginResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            Console.WriteLine($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await LoginRequestGrpc.Login(request, channel);
                if (!responseGrpc.Details.Success)
                {
                    return BadRequest(new LoginResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                return Ok(new LoginResponse
                {
                    Token = responseGrpc.Token,
                    Message = "Login successful",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
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
                Console.WriteLine($"Error in gRPC discovery: {ex.Message}");
                return BadRequest(new VerifyUserResponse
                {
                    Token = null,
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            Console.WriteLine($"UserGrpcChannel = {channel}");

            try
            {
                var responseGrpc = await VerifyUserGrpc.VerifyUser(request.Token, channel);
                if (!responseGrpc.Details.Success)
                {
                    return BadRequest(new VerifyUserResponse
                    {
                        Token = null,
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                return Ok(new VerifyUserResponse
                {
                    Token = responseGrpc.User.Token,
                    Message = "Verification is successful",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
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