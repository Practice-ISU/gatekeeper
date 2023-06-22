namespace Api.Data.Api.Responses
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
