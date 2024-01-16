using System.Net;

namespace Exceptions;

public class PoiApiException : Exception
{
    private readonly HttpStatusCode? HttpStatusCode;

    public PoiApiException() {}

    public PoiApiException(string? message) : base(message) {}

    public PoiApiException(string? message, Exception? innerException) : base(message, innerException) {}

    public PoiApiException(string? message, Exception? innerException, HttpStatusCode? httpStatusCode) : base(message, innerException) 
    {
        HttpStatusCode = httpStatusCode;
    }
}