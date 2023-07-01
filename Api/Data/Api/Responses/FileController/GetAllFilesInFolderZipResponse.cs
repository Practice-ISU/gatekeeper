namespace Api.Data.Api.Responses.FileController
{
    public class GetAllFilesInFolderZipResponse
    {
        public string? ZipName { get; set; }
        public string? Url { get; set; }
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }

        public GetAllFilesInFolderZipResponse(string? zipName, string? url, string? message, bool? isSuccess)
        {
            ZipName = zipName;
            Url = url;
            Message = message;
            IsSuccess = isSuccess;
        }

        public GetAllFilesInFolderZipResponse(string? zipName, string? url, string? message) : this(zipName, url, message, true) { }
        public GetAllFilesInFolderZipResponse(string? message) : this(null, null, message, false) { }
    }
}
