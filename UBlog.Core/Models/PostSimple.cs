namespace UBlog.Core.Models;

public class PostSimple
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public byte[] Image { get; init; }
    public string Text { get; init; }
    public string Title { get; init; }
    public DateTime CreationTime { get; init; }
}