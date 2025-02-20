namespace TravelPlanner.Domain.Exceptions;

public class InvalidCredentialsException: Exception
{

    public InvalidCredentialsException(): base("Invalid Credentials") {}
    
}