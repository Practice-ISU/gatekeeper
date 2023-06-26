using System.Text.Json.Serialization;

namespace Api.Data.Api.Responses
{
    public class VerifyUserResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }

    }
}
