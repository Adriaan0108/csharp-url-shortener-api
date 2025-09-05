namespace csharp_chat_api.Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "") : base(message)
    {
    }

    public ForbiddenException(string message, Exception inner) : base(message, inner)
    {
    }
}