namespace UBlog.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Mail { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    // ?
    //public byte[] Password { get; set; }

    public static User GetOne()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Login = "Login",
            Mail = "Mail",
            Name = "Name",
            Bio = "Bio"
        };
    }
}