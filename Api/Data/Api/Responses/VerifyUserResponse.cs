using System.Text.Json.Serialization;

namespace Api.Data.Api.Responses
{
    /// <summary>
    /// Represents the response generated when verifying a user.
    /// </summary>
    public class VerifyUserResponse
    {
        /// <summary>
        /// Gets or sets the authentication token generated upon successful verification of user.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets a message describing the result of the verification attempt.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the verification attempt was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUserResponse"/> class with the specified token, message and success status.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="message">The message describing the verification result.</param>
        /// <param name="isSuccess">A value indicating whether the verification was successful or not.</param>
        public VerifyUserResponse(string? token, string? message, bool isSuccess)
        {
            Token = token;
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUserResponse"/> class with only a message passed in as a parameter.
        /// </summary>
        /// <param name="message">The message describing the verification result.</param>
        public VerifyUserResponse(string? message)
            : this(null, message, false)
        {
            // Constructor for passing only message to initialize the VerifyUserResponse object.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUserResponse"/> class with both a token and message passed in as parameters.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="message">The message describing the verification result.</param>
        public VerifyUserResponse(string? token, string? message)
            : this(token, message, true)
        {
            // Constructor for passing both token and message to initialize the VerifyUserResponse object.
        }
    }
}
