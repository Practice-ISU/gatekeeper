namespace Api.Data.Api.Responses.UserController
{
    /// <summary>
    /// Represents the response generated when a user logs in.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the authentication token generated upon successful login.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets a message describing the result of the login attempt.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the login attempt was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class with the specified token, message and success status.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="message">The message describing the login result.</param>
        /// <param name="isSuccess">A value indicating whether the login was successful or not.</param>
        public LoginResponse(string? token, string? message, bool isSuccess)
        {
            Token = token;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class with only a message passed in as a parameter.
        /// </summary>
        /// <param name="message">The message describing the login result.</param>
        public LoginResponse(string? message)
            : this(null, message, false)
        {
            // Constructor for passing only message to initialize the LoginResponse object.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class with both a token and message passed in as parameters.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="message">The message describing the login result.</param>
        public LoginResponse(string? token, string? message)
            : this(token, message, true)
        {
            // Constructor for passing both token and message to initialize the LoginResponse object.
        }

    }
}
