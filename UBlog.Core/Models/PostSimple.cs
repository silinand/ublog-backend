using UBlog.Core.Interfaces;

namespace UBlog.Core.Models;

public class PostSimple : IDatabaseEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public byte[] Image { get; init; }
    public string Text { get; init; }
    public string Title { get; init; }
    public DateTime CreationTime { get; init; }
}