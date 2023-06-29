using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FolderController
{
    public class GetAllFoldersRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Token);
        }
    }
}
