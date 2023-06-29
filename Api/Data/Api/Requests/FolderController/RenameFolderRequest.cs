using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FolderController
{
    public class RenameFolderRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("folderId")]
        public long? FolderId { get; set; }

        [JsonPropertyName("newFolderName")]
        public string? NewFolderName { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null && NewFolderName != null;
        }
    }
}