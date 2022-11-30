namespace SerilogElasticKibana.Api.Exceptions;

[Serializable]
public class ClientRequestException : Exception
{
    public string ErrorCode { get; set; }
    public int StatusCode { get; }
    public ClientRequestException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public ClientRequestException(string message, int statusCode, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}