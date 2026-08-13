namespace Forum.Exceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string message)
        : base(message)
    {
    }
}