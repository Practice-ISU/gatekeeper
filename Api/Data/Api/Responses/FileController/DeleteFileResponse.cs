namespace Api.Data.Api.Responses.FileController
{
    /// <summary>
    /// Represents the response for deleting a file.
    /// </summary>
    public class DeleteFileResponse
    {
        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFileResponse"/> class.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        public DeleteFileResponse(string? message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFileResponse"/> class with only a message and failure status.
        /// </summary>
        /// <param name="message">The message associated with the response.</param>
        public DeleteFileResponse(string? message) : this(message, false)
        {
            // Constructor for passing only the message to initialize the DeleteFileResponse object.
        }
    }
}
