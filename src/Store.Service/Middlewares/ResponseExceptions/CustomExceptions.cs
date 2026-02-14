using System;

namespace Store.Service.Middlewares.ResponseExceptions;


public class CustomExceptions : Response
{
    public CustomExceptions(int statusCode, string message, string? details = null) : base(statusCode, message)
    {
        Details = details;
    }

    public string? Details { get; set; }
}
