using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FolderController
{
    public class AddFolderRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("folderName")]
        public string? FolderName { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderName != null;
        }
    }
}
