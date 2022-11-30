namespace SerilogElasticKibana.Api.Exceptions;

[Serializable]
public abstract class BaseException : Exception
{
    public virtual string ErrorCode { get; }
    public virtual int StatusCode { get; }

    public BaseException() { }

    protected BaseException(int statusCode)
    {
        StatusCode = statusCode;
        ErrorCode = string.Empty;
    }

    protected BaseException(int statusCode, string errorCode)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

}
