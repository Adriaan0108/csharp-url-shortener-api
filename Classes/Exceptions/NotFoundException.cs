namespace csharp_chat_api.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "") : base(message)
    {
    }

    public NotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}