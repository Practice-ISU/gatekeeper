namespace Api.Data.Api.Responses.FileController
{
    /// <summary>
    /// Represents the response for adding a file.
    /// </summary>
    public class AddFileResponse
    {
        /// <summary>
        /// Gets or sets the ID of the file.
        /// </summary>
        public Int64? FileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the folder that the file belongs to.
        /// </summary>
        public Int64? FolderId { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFileResponse"/> class.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="folderId">The ID of the folder that the file belongs to.</param>
        /// <param name="message">The message associated with the response.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public AddFileResponse(long? fileId, string? fileName, Int64? folderId, string? message, bool? isSuccess)
        {
            FileId = fileId;
            FileName = fileName;
            FolderId = folderId;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFileResponse"/> class with only a message.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        public AddFileResponse(string? message) : this(null, null, null, message, false)
        {
            // Constructor for passing only the message to initialize the AddFileResponse object.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFileResponse"/> class with specific values and success status.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="folderId">The ID of the folder that the file belongs to.</param>
        /// <param name="message">The message associated with the response.</param>
        public AddFileResponse(long? fileId, string? fileName, Int64? folderId, string? message) : this(fileId, fileName, folderId, message, true)
        {
            // Constructor for passing both folderId and message to initialize the AddFileResponse object.
        }
    }
}
