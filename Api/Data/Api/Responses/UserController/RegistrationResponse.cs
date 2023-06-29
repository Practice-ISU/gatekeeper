namespace Api.Data.Api.Responses.UserController
{
    /// <summary>
    /// Represents a response returned by the API after a registration attempt is made.
    /// </summary>
    public class RegistrationResponse
    {
        /// <summary>
        /// Gets or sets the generated token associated with the registered account, if successful.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the message describing the result of the registration attempt.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the registration attempt was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResponse"/> class.
        /// </summary>
        /// <param name="token">The generated token associated with the registered account, if successful.</param>
        /// <param name="message">The message describing the result of the registration attempt.</param>
        /// <param name="isSuccess">A value indicating whether the registration attempt was successful.</param>
        public RegistrationResponse(string? token, string? message, bool isSuccess)
        {
            Token = token;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResponse"/> class with a message only.
        /// </summary>
        /// <param name="message">The message describing the result of the registration attempt.</param>
        public RegistrationResponse(string? message)
            : this(null, message, false)
        {
            // Constructor with message parameter only
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResponse"/> class with a token and message.
        /// </summary>
        /// <param name="token">The generated token associated with the registered account.</param>
        /// <param name="message">The message describing the result of the registration attempt.</param>
        public RegistrationResponse(string? token, string? message)
            : this(token, message, true)
        {
            // Constructor with token and message parameters
        }

    }
}