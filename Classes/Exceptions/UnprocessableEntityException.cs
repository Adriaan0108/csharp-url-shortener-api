namespace csharp_chat_api.Common.Exceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message = "") : base(message)
    {
    }

    public UnprocessableEntityException(string message, Exception inner) : base(message, inner)
    {
    }
}