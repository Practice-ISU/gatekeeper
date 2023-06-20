using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    using Api.Data.Api.Requests;
    using Api.Data.Api.Responses;

    [Route("/auth")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("/register")]
        public IActionResult Register(RegistrationRequest request)
        {

            if (request.IsValid())
            {
                return BadRequest(new RegistrationResponse
                {
                    Message = "Invalid registration data",
                    IsSuccess = false
                });
            }

            // TODO : Check user exists by user-service

            return Ok(new RegistrationResponse
            {
                Message = "User registered successfully",
                IsSuccess = true
            });
        }
    }
}
