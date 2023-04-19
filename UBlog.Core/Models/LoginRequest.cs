namespace UBlog.Core.Models;

public class LoginRequest
{
    public string Username { get; }

    public string Password { get; }

    public bool Remember { get; }
}