namespace Application.Common.Exceptions;

[Serializable]
public class ClientRequestException : Exception
{
    public ClientRequestException(List<string> messages, int statusCode, string errorCode)
    {
        Messages = messages;
        Message = string.Join(Environment.NewLine, messages);
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    public ClientRequestException(List<string> messages, int statusCode)
    {
        Messages = messages;
        Message = string.Join(Environment.NewLine, messages);
        StatusCode = statusCode;
    }

    public string ErrorCode { get; set; }
    public int StatusCode { get; }
    public override string Message { get; }
    public List<string> Messages { get; }
}