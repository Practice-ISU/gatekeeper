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
                string? tempChannel = await GetChannelRequest.GetChannel("UserGrpcService")!;
                if (tempChannel != null)
                {
                    channel = tempChannel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserGrpcService {ex.Message}");
                return BadRequest(new RegistrationResponse
                {
                    Message = "Service not available",
                    IsSuccess = false
                });
            }

            Console.WriteLine($"UserGrpsChanel = {channel}");
            UserRegisterResponse responseGrpc = await RegistrationRequestGrpc.Registration(request, channel);
            bool statusRegistration = responseGrpc.Details.Success;

            if (!statusRegistration)
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
    }
}
