using System.Text.Json.Serialization;

namespace Api.Data.Api.Requests.FileController
{
    public class DeleteFileRequest
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("fileId")]
        public Int64? FileId { get; set; }

        public bool IsValid()
        {
            return Token != null && FileId != null;
        }
    }
}
