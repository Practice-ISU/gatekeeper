using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.UserController
{
    public class RegistrationRequest
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        public bool IsValid()
        {
            return Username != null && Password != null;
        }
    }
}
