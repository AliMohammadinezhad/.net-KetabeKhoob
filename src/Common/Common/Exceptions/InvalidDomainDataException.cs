namespace Common.Exceptions;

public class InvalidDomainDataException : BaseDomainException
{
    public InvalidDomainDataException()
    {
    }

    public InvalidDomainDataException(string message) : base(message)
    {
    }


}