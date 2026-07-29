namespace Domain.Exceptions
{
    public class AlreadyExistException(string message) : ApplicationException(message)
    {
    }
}
