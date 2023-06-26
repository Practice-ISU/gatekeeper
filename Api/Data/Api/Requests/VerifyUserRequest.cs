using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests
{
    public class VerifyUserRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        public bool IsValid()
        {
            return Token != null;
        }
    }
}
