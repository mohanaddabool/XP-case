namespace XPAssignment.authentication;

public interface IAuthentication
{
    string Authenticate(string? emailAddress, string? password);
}