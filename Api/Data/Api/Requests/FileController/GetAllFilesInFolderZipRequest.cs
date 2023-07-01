using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FileController
{
    public class GetAllFilesInFolderZipRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("folderId")]
        public Int64? FolderId { get; set; }

        public bool IsValid()
        {
            return Token != null && FolderId != null;
        }
    }
}
