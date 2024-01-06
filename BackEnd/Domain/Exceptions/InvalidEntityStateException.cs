namespace Domain.Exceptions;

/// <summary>
/// An exception caused because an action would cause a domain object
/// to be in an invalid state.
/// </summary>
public class InvalidEntityStateException : Exception
{
    public InvalidEntityStateException(string errorMessage)
    : base(errorMessage)
    {
        
    }
}