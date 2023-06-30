namespace Api.Data.Api.Responses.FileController
{
    /// <summary>
    /// Represents a response object containing file information in Base64 format.
    /// </summary>
    public class GetFileBase64Response
    {
        /// <summary>
        /// Gets or sets the ID of the file.
        /// </summary>
        public Int64? FileId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the folder.
        /// </summary>
        public Int64? FolderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the file content in Base64 format.
        /// </summary>
        public string? Base64 { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileBase64Response"/> class.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        /// <param name="folderId">The ID of the folder.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="base64">The file content in Base64 format.</param>
        /// <param name="message">The response message.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public GetFileBase64Response(long? fileId, long? folderId, string? fileName, string? base64, string? message, bool? isSuccess)
        {
            FileId = fileId;
            FolderId = folderId;
            FileName = fileName;
            Base64 = base64;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileBase64Response"/> class with only a response message.
        /// </summary>
        /// <param name="message">The response message.</param>
        public GetFileBase64Response(string? message) : this(null, null, null, null, message, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileBase64Response"/> class with specified file information and a response message indicating success.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        /// <param name="folderId">The ID of the folder.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="base64">The file content in Base64 format.</param>
        /// <param name="message">The response message.</param>
        public GetFileBase64Response(long? fileId, long? folderId, string? fileName, string? base64, string? message) : this(fileId, folderId, fileName, base64, message, true) { }
    }
}
