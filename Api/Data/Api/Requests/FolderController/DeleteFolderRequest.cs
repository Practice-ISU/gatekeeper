using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FolderController
{
    public class DeleteFolderRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("folderId")]
        public long? FolderId { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null;
        }
    }
}
