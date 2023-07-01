using System.Buffers.Text;

namespace Api.Data.Api.Responses.FileController
{
    public class GetFileResponse
    {
        public Int64? FileId { get; set; }
        public Int64? FolderId { get; set; }
        public string? FileName { get; set; }
        public string? Url { get; set; }
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }

        public GetFileResponse(long? fileId, long? folderId, string? fileName, string? url, string? message, bool? isSuccess)
        {
            FileId = fileId;
            FolderId = folderId;
            FileName = fileName;
            Url = url;
            Message = message;
            IsSuccess = isSuccess;
        }

        public GetFileResponse(string? message) : this(null, null, null, null, message, false) { }

        public GetFileResponse(long? fileId, long? folderId, string? fileName, string? url, string? message) : this(fileId, folderId, fileName, url, message, true) { }


    }
}
