using System.ComponentModel.DataAnnotations;
using UBlog.Core.Models;

namespace UBlog.EntityFramework.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    
    //public string Password { get; set; }
    
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
        Email = Email,
        Name = Name,
        Bio = Bio
    };

    public void Update(UserSimple user)
    {
        Email = user.Email;
        Name = user.Name;
        Bio = user.Bio;
    }
}