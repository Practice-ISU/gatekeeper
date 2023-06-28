namespace Api.Data.Api.Responses
{
    /// <summary>
    /// Represents the response generated when a folder is deleted.
    /// </summary>
    public class DeleteFolderResponse
    {
        /// <summary>
        /// Gets or sets a message describing the result of the folder deletion.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the folder deletion was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFolderResponse"/> class with the specified message and success status.
        /// </summary>
        /// <param name="message">The message describing the folder deletion result.</param>
        /// <param name="isSuccess">A value indicating whether the folder deletion was successful or not.</param>
        public DeleteFolderResponse(string? message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFolderResponse"/> class with only a message passed in as a parameter.
        /// The success status is set to false.
        /// </summary>
        /// <param name="message">The message describing the folder deletion result.</param>
        public DeleteFolderResponse(string? message)
            : this(message, false)
        {
            // Constructor for passing only message to initialize the DeleteFolderResponse object.
        }
    }
}
