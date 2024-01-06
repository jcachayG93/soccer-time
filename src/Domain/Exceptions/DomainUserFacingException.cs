namespace Domain.Exceptions;

public class DomainUserFacingException : Exception
{
    public DomainUserFacingException(
        string errorMessage) : base(errorMessage)
    {
        
    }
}