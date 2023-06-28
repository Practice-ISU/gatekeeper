namespace Api.Data.Api.Responses
{
    /// <summary>
    /// Represents the response generated when a folder is added.
    /// </summary>
    public class AddFolderResponse
    {
        /// <summary>
        /// Gets or sets the ID of the added folder.
        /// </summary>
        public Int64? FolderId { get; set; }

        /// <summary>
        /// Gets or sets a message describing the result of the folder addition.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the folder addition was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFolderResponse"/> class with the specified folder ID, message, and success status.
        /// </summary>
        /// <param name="folderId">The ID of the added folder.</param>
        /// <param name="message">The message describing the folder addition result.</param>
        /// <param name="isSuccess">A value indicating whether the folder addition was successful or not.</param>
        public AddFolderResponse(Int64? folderId, string? message, bool isSuccess)
        {
            FolderId = folderId;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFolderResponse"/> class with only a message passed in as a parameter.
        /// The folder ID is set to null and the success status is set to false.
        /// </summary>
        /// <param name="message">The message describing the folder addition result.</param>
        public AddFolderResponse(string? message)
            : this(null, message, false)
        {
            // Constructor for passing only message to initialize the AddFolderResponse object.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFolderResponse"/> class with both a folder ID and message passed in as parameters.
        /// The success status is set to true.
        /// </summary>
        /// <param name="folderId">The ID of the added folder.</param>
        /// <param name="message">The message describing the folder addition result.</param>
        public AddFolderResponse(Int64 folderId, string? message)
            : this(folderId, message, true)
        {
            // Constructor for passing both folderId and message to initialize the AddFolderResponse object.
        }
    }
}
