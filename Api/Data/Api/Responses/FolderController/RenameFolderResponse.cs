namespace Api.Data.Api.Responses.FolderController
{
    /// <summary>
    /// Represents the response generated when a folder is renamed.
    /// </summary>
    public class RenameFolderResponse
    {
        /// <summary>
        /// Gets or sets the ID of the renamed folder.
        /// </summary>
        public long? FolderId { get; set; }

        /// <summary>
        /// Gets or sets a message describing the result of the folder rename.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the folder rename was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenameFolderResponse"/> class with the specified folder ID, message, and success status.
        /// </summary>
        /// <param name="folderId">The ID of the renamed folder.</param>
        /// <param name="message">The message describing the folder rename result.</param>
        /// <param name="isSuccess">A value indicating whether the folder rename was successful or not.</param>
        public RenameFolderResponse(long? folderId, string? message, bool isSuccess)
        {
            FolderId = folderId;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenameFolderResponse"/> class with only a message passed in as a parameter.
        /// The folder ID is set to null and the success status is set to false.
        /// </summary>
        /// <param name="message">The message describing the folder rename result.</param>
        public RenameFolderResponse(string? message)
            : this(null, message, false)
        {
            // Constructor for passing only message to initialize the RenameFolderResponse object.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenameFolderResponse"/> class with both a folder ID and message passed in as parameters.
        /// The success status is set to true.
        /// </summary>
        /// <param name="folderId">The ID of the renamed folder.</param>
        /// <param name="message">The message describing the folder rename result.</param>
        public RenameFolderResponse(long folderId, string? message)
            : this(folderId, message, true)
        {
            // Constructor for passing both folderId and message to initialize the RenameFolderResponse object.
        }
    }
}
