using System.Net;

namespace PriceFeed.Domain.Exceptions.Base;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
