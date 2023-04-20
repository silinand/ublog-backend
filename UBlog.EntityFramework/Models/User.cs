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
}