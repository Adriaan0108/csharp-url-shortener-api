namespace csharp_chat_api.Common.Exceptions;

public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException(string message = "") : base(message)
    {
    }

    public ServiceUnavailableException(string message, Exception inner) : base(message, inner)
    {
    }
}