using UBlog.Core.Interfaces;
using UBlog.Core.Models;

namespace UBlog.EntityFramework.Models;

public class User : ISimplify<UserSimple>, IDatabaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public List<Post> Posts { get; set; }

    public User()
    { }
    
    internal User(UserSimple simple)
    {
        Update(simple);
    }
    
    public UserSimple Simplify() => new()
    {
        Id = Id,
        Username = Username,
        Email = Email,
        Name = Name,
        Bio = Bio
    };

    public void Update(UserSimple user)
    {
        Username = user.Username;
        Email = user.Email;
        Name = user.Name;
        Bio = user.Bio;
    }
}