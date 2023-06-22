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
                        Message = responseGrpc.Details.Mess,
                        IsSuccess = false
                    });
                }

                return Ok(new RegistrationResponse
                {
                    Message = "User registered successfully",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return BadRequest(new RegistrationResponse
                {
                    Message = "Registration failed.",
                    IsSuccess = false
                });
            }
        }

    }
}
