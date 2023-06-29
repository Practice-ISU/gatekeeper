using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FileController
{
    public class RenameFileRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("fileId")]
        public Int64? FileId { get; set; }

        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

        public bool IsValid()
        {
            return Token != null && FileId != null && FileName != null;
        }
    }
}
