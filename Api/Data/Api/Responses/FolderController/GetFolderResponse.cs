namespace Api.Data.Api.Responses.FolderController
{
    /// <summary>
    /// Represents the response generated when a folder is renamed.
    /// </summary>
    public class GetFolderResponse
    {
        /// <summary>
        /// Gets or sets a message describing the result of the folder rename.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the folder rename was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFolderResponse"/> class with the specified message and success status.
        /// </summary>
        /// <param name="message">The message describing the folder rename result.</param>
        /// <param name="isSuccess">A value indicating whether the folder rename was successful or not.</param>
        public GetFolderResponse(string? message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFolderResponse"/> class with only a message passed in as a parameter.
        /// The success status is set to false.
        /// </summary>
        /// <param name="message">The message describing the folder rename result.</param>
        public GetFolderResponse(string? message)
            : this(message, false)
        {
            // Constructor for passing only message to initialize the GetFolderResponse object.
        }
    }
}