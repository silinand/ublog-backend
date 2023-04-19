using System.ComponentModel.DataAnnotations;
using UBlog.Core.Models;

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
    //public List<Like> Likes { get; set; }

    public Post() {}
    
    internal Post(PostSimple simple)
    {
        Update(simple);
    }

    public PostSimple Simplify()
    {
        return new PostSimple()
        {
            Id = Id,
            UserId = UserId,
            Title = Title,
            Text = Text,
            Image = Image,
            CreationTime = CreationTime
        };
    }

    public void Update(PostSimple post)
    {
        Title = post.Title;
        Text = post.Text;
        Image = post.Image;
        CreationTime = post.CreationTime;
    }
    
    
    //public List<Comment> Comments { get; set; }
}