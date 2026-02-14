using System;

namespace Store.Service.Middlewares.ResponseExceptions;

public class Response
{
    public Response(int statusCode , string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetExceptionMessage();
    }

    public int StatusCode { get; set; }
    public string ? Message { get; set; }

    private string GetExceptionMessage()
    =>  StatusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        403 => "Forbidden",
        404 => "Not Found",
        500 => "Internal Server Error",
        _ => "Unknown Status Code"
    };
}
