namespace API.Application.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string DetailedMessage { get; set; } = string.Empty;
    }
}
