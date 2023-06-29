using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FileController
{
    public class AddFileRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

        [JsonPropertyName("folderId")]
        public Int64? FolderId { get; set; }

        [JsonPropertyName("base64")]
        public string? Base64 { get; set; }

        [JsonPropertyName("format")]
        public string? Format { get; set; }

        public bool IsValid()
        {
            return Token != null && FileName != null && FolderId != null && Base64 != null && Format != null;
        }
    }
}
