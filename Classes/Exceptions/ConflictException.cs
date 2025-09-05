namespace csharp_chat_api.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message = "") : base(message)
    {
    }

    public ConflictException(string message, Exception inner) : base(message, inner)
    {
    }
}