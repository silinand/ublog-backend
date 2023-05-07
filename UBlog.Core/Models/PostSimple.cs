namespace UBlog.Core.Models;

public class PostSimple
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public string ImageUrl { get; set; }
    public string Text { get; init; }
    public string Title { get; init; }
    public DateTime CreationTime { get; init; }
    public int Likes { get; set; }
    public bool IsLiked { get; set; }
}