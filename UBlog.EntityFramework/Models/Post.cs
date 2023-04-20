using System.ComponentModel.DataAnnotations;

namespace UBlog.EntityFramework.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; }
    public byte[] Image { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
}